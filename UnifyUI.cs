#region Copyright and License
// -----------------------------------------------------------------------------------------------------------------
// 
// UnifyUI.cs
// 
// Copyright © 2020 Nivloc Enterprises Ltd.  All rights reserved.
// 
// -----------------------------------------------------------------------------------------------------------------
// 
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
// 
// 
// 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.DeviceSupport;
using Crestron.SimplSharp.CrestronIO;
using Crestron.SimplSharp.CrestronXml;
using Crestron.SimplSharp.CrestronXmlLinq;
using Stream = Crestron.SimplSharp.CrestronIO.Stream;

namespace Crestron.SimplSharpPro.Helpers
	{
	public delegate void UnifiedSigEventHandler (UnifiedUIDevice uiDevice, UnifiedSigEventArgs args);

	public class UnifiedUIDevice
		{
		private readonly object m_device;
		private readonly CrestronDeviceWithEvents m_cde;
		private readonly SmartObject m_so;
		private readonly BasicTriList m_btl;
		private readonly Dictionary<string, uint> dictBoolSigMap;
		private readonly Dictionary<string, uint> dictUShortSigMap;
		private readonly Dictionary<string, uint> dictStringSigMap;

		private DeviceBooleanInputCollection m_booleanInput;
		private DeviceBooleanOutputCollection m_booleanOutput;
		private DeviceUShortInputCollection m_ushortInput;
		private DeviceUShortOutputCollection m_ushortOutput;
		private DeviceStringInputCollection m_stringInput;
		private DeviceStringOutputCollection m_stringOutput;

		public DeviceBooleanInputCollection BooleanInput
			{
			get
				{
				if (m_booleanInput == null)
					{
					if (m_cde != null)
						m_booleanInput = m_cde.BooleanInput;
					else if (m_so != null)
						m_booleanInput = m_so.BooleanInput;
					else if (m_btl != null)
						m_booleanInput = m_btl.BooleanInput;
					}
				return m_booleanInput;
				}
			}

		public DeviceBooleanOutputCollection BooleanOutput
			{
			get
				{
				if (m_booleanOutput == null)
					{
					if (m_cde != null)
						m_booleanOutput = m_cde.BooleanOutput;
					else if (m_so != null)
						m_booleanOutput = m_so.BooleanOutput;
					else if (m_btl != null)
						m_booleanOutput = m_btl.BooleanOutput;
					}
				return m_booleanOutput;
				}
			}

		public DeviceUShortInputCollection UShortInput
			{
			get
				{
				if (m_ushortInput == null)
					{
					if (m_cde != null)
						m_ushortInput = m_cde.UShortInput;
					else if (m_so != null)
						m_ushortInput = m_so.UShortInput;
					else if (m_btl != null)
						m_ushortInput = m_btl.UShortInput;
					}
				return m_ushortInput;
				}
			}

		public DeviceUShortOutputCollection UShortOutput
			{
			get
				{
				if (m_ushortOutput == null)
					{
					if (m_cde != null)
						m_ushortOutput = m_cde.UShortOutput;
					else if (m_so != null)
						m_ushortOutput = m_so.UShortOutput;
					else if (m_btl != null)
						m_ushortOutput = m_btl.UShortOutput;
					}
				return m_ushortOutput;
				}
			}

		public DeviceStringInputCollection StringInput
			{
			get
				{
				if (m_stringInput == null)
					{
					if (m_cde != null)
						m_stringInput = m_cde.StringInput;
					else if (m_so != null)
						m_stringInput = m_so.StringInput;
					else if (m_btl != null)
						m_stringInput = m_btl.StringInput;
					}
				return m_stringInput;
				}
			}

		public DeviceStringOutputCollection StringOutput
			{
			get
				{
				if (m_stringOutput == null)
					{
					if (m_cde != null)
						m_stringOutput = m_cde.StringOutput;
					else if (m_so != null)
						m_stringOutput = m_so.StringOutput;
					else if (m_btl != null)
						m_stringOutput = m_btl.StringOutput;
					}
				return m_stringOutput;
				}
			}

		public object Device
			{
			get { return m_device; }
			}

		public CrestronDeviceWithEvents CrestronDeviceWithEvents
			{
			get { return m_cde; }
			}

		public SmartObject SmartObject
			{
			get { return m_so; }
			}

		public BasicTriList BasicTriList
			{
			get { return m_btl; }
			}

		public event UnifiedSigEventHandler SigChange;

		public UnifiedUIDevice (object baseDevice)
			: this (baseDevice, null, null)
			{
			}

		private static char[] NumChars = { ' ', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

		public UnifiedUIDevice (object baseDevice, Stream streamMap, string deviceId)
			{
			m_device = baseDevice;

			m_cde = m_device as CrestronDeviceWithEvents;
			if (m_cde != null)
				{
				m_cde.SigChange += cde_SigChange;
				}
			else
				{
				m_so = m_device as SmartObject;
				if (m_so != null)
					{
					m_so.SigChange += so_SigChange;
					}
				else
					{
					m_btl = m_device as BasicTriList;
					if (m_btl != null)
						{
						m_btl.SigChange += btl_SigChange;
						}
					else
						throw new NotSupportedException (String.Format ("Base device of type {0} not supported", baseDevice.GetType ().Name));
					}
				}

			if (streamMap == null)
				return;

			XDocument xd;
			using (var xr = new XmlReader (streamMap))
				{
				try
					{
					xd = XDocument.Load (xr);
					}
				catch (Exception ex)
					{
					ErrorLog.Exception ("Exception while loading device map stream", ex);
					return;
					}
				}

			XElement xe = xd.Elements ().SingleOrDefault (xet => xet.Name == "device" && xet.Attribute ("name") != null && xet.Attribute ("name").Value == deviceId);
			if (xe == null)
				return;

			dictBoolSigMap = GetSigMap (xe.Element ("bool"));
			dictUShortSigMap = GetSigMap (xe.Element ("ushort"));
			dictStringSigMap = GetSigMap (xe.Element ("string"));
			}

		private Dictionary<string, uint> GetSigMap (XElement xeType)
			{
			var q = from e in xeType.Descendants () where e.Name == "map" && e.Attribute ("join") != null && e.Attribute ("name") != null && e.Attribute ("join").Value.Trim (NumChars).Length != 0 select e;

			return q.ToDictionary (e => e.Attribute ("name").Value, e => UInt32.Parse (e.Attribute ("join").Value));
			}

		void btl_SigChange (GenericBase currentDevice, SigEventArgs args)
			{
			if (SigChange != null)
				SigChange (this, new UnifiedSigEventArgs (args));

			OnSigChange (args);
			}

		void so_SigChange (GenericBase currentDevice, SmartObjectEventArgs args)
			{
			if (SigChange != null)
				SigChange (this, new UnifiedSigEventArgs (args));

			OnSigChange (args);
			}

		void cde_SigChange (CrestronDeviceWithEvents currentDevice, SigEventArgs args)
			{
			if (SigChange != null)
				SigChange (this, new UnifiedSigEventArgs (args));

			OnSigChange (args);
			}

		private void OnSigChange (SigEventArgs args)
			{
			UnifiedSigEventArgs usea = new UnifiedSigEventArgs (args);

			switch (args.Event)
				{
				case eSigEvent.BoolOutputSigsCleared:
					BooleanOutput.Where (bo => bo.UserObject is UnifiedSigUserObject && ((UnifiedSigUserObject)bo.UserObject).UnifiedSigEvents != null).Select (bo => ((UnifiedSigUserObject)bo.UserObject).UnifiedSigEvents).ToList ().ForEach (lc => lc.FindAll (u => u.SigChange != null && (u.eSigEvent == eSigEvent.NA || u.eSigEvent == eSigEvent.BoolChange)).ForEach (u => u.SigChange (this, usea)));
					break;
				case eSigEvent.UShortOutputSigsCleared:
					UShortOutput.Where (uo => uo.UserObject is UnifiedSigUserObject && ((UnifiedSigUserObject)uo.UserObject).UnifiedSigEvents != null).Select (uo => ((UnifiedSigUserObject)uo.UserObject).UnifiedSigEvents).ToList ().ForEach (lc => lc.FindAll (u => u.SigChange != null && (u.eSigEvent == eSigEvent.NA || u.eSigEvent == eSigEvent.UShortChange)).ForEach (u => u.SigChange (this, usea)));
					break;
				case eSigEvent.StringOutputSigsCleared:
					StringOutput.Where (so => so.UserObject is UnifiedSigUserObject && ((UnifiedSigUserObject)so.UserObject).UnifiedSigEvents != null).Select (so => ((UnifiedSigUserObject)so.UserObject).UnifiedSigEvents).ToList ().ForEach (lc => lc.FindAll (u => u.SigChange != null && (u.eSigEvent == eSigEvent.NA || u.eSigEvent == eSigEvent.StringChange)).ForEach (u => u.SigChange (this, usea)));
					break;
				default:
					var uuo = args.Sig.UserObject as UnifiedSigUserObject;
					if (uuo == null)
						return;

					if (uuo.UnifiedSigEvents == null || uuo.UnifiedSigEvents.Count == 0)
						{
						args.Sig.UserObject = uuo.UserObject;
						return;
						}

					switch (args.Sig.Type)
						{
						case eSigType.Bool:
							usea.BoolValue = args.Sig.BoolValue;
							break;
						case eSigType.UShort:
							usea.UShortValue = args.Sig.UShortValue;
							break;
						case eSigType.String:
							usea.StringValue = args.Sig.StringValue;
							break;
						}

					uuo.UnifiedSigEvents.FindAll (u => u.SigChange != null && (u.eSigEvent == eSigEvent.NA || u.eSigEvent == args.Event)).ForEach (u => u.SigChange (this, usea));
					break;
				}
			}

		public void DetachAllSigEventHandlers ()
			{
			BooleanOutput.Where (bo => bo.UserObject is UnifiedSigUserObject).ToList ().ForEach (bo => bo.UserObject = ((UnifiedSigUserObject)bo.UserObject).UserObject);
			UShortOutput.Where (uo => uo.UserObject is UnifiedSigUserObject).ToList ().ForEach (uo => uo.UserObject = ((UnifiedSigUserObject)uo.UserObject).UserObject);
			StringOutput.Where (so => so.UserObject is UnifiedSigUserObject).ToList ().ForEach (so => so.UserObject = ((UnifiedSigUserObject)so.UserObject).UserObject);
			UShortInput.Where (ui => ui.UserObject is UnifiedSigUserObject).ToList ().ForEach (ui => ui.UserObject = ((UnifiedSigUserObject)ui.UserObject).UserObject);
			}

		public void AttachBooleanEventHandler (uint index, UnifiedSigEventHandler handler)
			{
			AttachBooleanEventHandler (index, handler, eSigEvent.BoolChange);
			}

		public void AttachBooleanEventHandler (uint index, UnifiedSigEventHandler handler, eSigEvent eventType)
			{
			BooleanOutput[index].AttachSigEventHandler (handler, eventType);
			}

		public void AttachBooleanEventHandler (string name, UnifiedSigEventHandler handler)
			{
			AttachBooleanEventHandler (name, handler, eSigEvent.BoolChange);
			}

		public void AttachBooleanEventHandler (string name, UnifiedSigEventHandler handler, eSigEvent eventType)
			{
			BooleanOutput[name].AttachSigEventHandler (handler, eventType);
			}

		public void AttachUShortEventHandler (uint index, UnifiedSigEventHandler handler)
			{
			AttachUShortEventHandler (index, handler, eSigEvent.UShortChange);
			}

		public void AttachUShortEventHandler (uint index, UnifiedSigEventHandler handler, eSigEvent eventType)
			{
			UShortOutput[index].AttachSigEventHandler (handler, eventType);
			}

		public void AttachUShortEventHandler (string name, UnifiedSigEventHandler handler)
			{
			AttachBooleanEventHandler (name, handler, eSigEvent.UShortChange);
			}

		public void AttachUShortEventHandler (string name, UnifiedSigEventHandler handler, eSigEvent eventType)
			{
			UShortOutput[name].AttachSigEventHandler (handler, eventType);
			}

		public void AttachUShortRampEventHandler (uint index, UnifiedSigEventHandler handler)
			{
			AttachUShortRampEventHandler (index, handler, eSigEvent.UShortInputRamping);
			}

		public void AttachUShortRampEventHandler (uint index, UnifiedSigEventHandler handler, eSigEvent eventType)
			{
			UShortInput[index].AttachSigEventHandler (handler, eventType);
			}

		public void AttachUShortRampEventHandler (string name, UnifiedSigEventHandler handler)
			{
			AttachUShortRampEventHandler (name, handler, eSigEvent.UShortInputRamping);
			}

		public void AttachUShortRampEventHandler (string name, UnifiedSigEventHandler handler, eSigEvent eventType)
			{
			UShortInput[name].AttachSigEventHandler (handler, eventType);
			}

		public void AttachStringEventHandler (uint index, UnifiedSigEventHandler handler)
			{
			AttachStringEventHandler (index, handler, eSigEvent.StringChange);
			}

		public void AttachStringEventHandler (uint index, UnifiedSigEventHandler handler, eSigEvent eventType)
			{
			StringOutput[index].AttachSigEventHandler (handler, eventType);
			}

		public void AttachStringEventHandler (string name, UnifiedSigEventHandler handler)
			{
			AttachStringEventHandler (name, handler, eSigEvent.StringChange);
			}

		public void AttachStringEventHandler (string name, UnifiedSigEventHandler handler, eSigEvent eventType)
			{
			StringOutput[name].AttachSigEventHandler (handler, eventType);
			}

		public void SetValue (uint index, string value)
			{
			SetStringValue (index, value);
			}

		public void SetValue (uint index, string format, params object[] args)
			{
			SetStringValue (index, format);
			}

		public void SetValue (string name, string value)
			{
			SetStringValue (name, value);
			}
		public void SetValue (string name, string format, params object[] args)
			{
			SetStringValue (name, format, args);
			}

		public void SetValue (uint index, ushort value)
			{
			SetUShortValue (index, value);
			}

		public void SetValue (string name, ushort value)
			{
			SetUShortValue (name, value);
			}

		public void SetValue (uint index, bool value)
			{
			SetBoolValue (index, value);
			}

		public void SetValue (string name, bool value)
			{
			SetBoolValue (name, value);
			}

		public void SetStringValue (uint index, string value)
			{
			StringInput.SetValue (index, value);
			}

		public void SetStringValue (uint index, bool propagate, string value)
			{
			StringInput.SetValue (index, propagate, value);
			}

		public void SetStringValue (string name, string value)
			{
			StringInput.SetValue (name, value);
			}

		public void SetStringValue (string name, bool propagate, string value)
			{
			StringInput.SetValue (name, value, propagate);
			}

		public void SetStringValue (uint index, string format, params object[] args)
			{
			StringInput.SetValue (index, format, args);
			}

		public void SetStringValue (uint index, bool propagate, string format, params object[] args)
			{
			StringInput.SetValue (index, propagate, format, args);
			}

		public void SetStringValue (string name, string format, params object[] args)
			{
			StringInput.SetValue (name, format, args);
			}

		public void SetStringValue (string name, bool propagate, string format, params object[] args)
			{
			StringInput.SetValue (name, propagate, format, args);
			}

		public void SetUShortValue (uint index, ushort value)
			{
			UShortInput.SetValue (index, value);
			}

		public void SetUShortValue (uint index, ushort value, bool propagate)
			{
			UShortInput.SetValue (index, value, propagate);
			}

		public void SetUShortValue (string name, ushort value)
			{
			UShortInput.SetValue (name, value);
			}

		public void SetUShortValue (string name, ushort value, bool propagate)
			{
			UShortInput.SetValue (name, value, propagate);
			}

		public void SetBoolValue (uint index, bool value)
			{
			BooleanInput[index].BoolValue = value;
			}

		public void SetBoolValue (string name, bool value)
			{
			BooleanInput[name].BoolValue = value;
			}

		public void SetTrue (uint index)
			{
			BooleanInput[index].BoolValue = true;
			}

		public void SetTrue (string name)
			{
			BooleanInput[name].BoolValue = true;
			}

		public void SetFalse (uint index)
			{
			BooleanInput[index].BoolValue = false;
			}

		public void SetFalse (string name)
			{
			BooleanInput[name].BoolValue = false;
			}

		public void Pulse (uint index)
			{
			BooleanInput[index].Pulse ();
			}

		public void Pulse (string name)
			{
			BooleanInput[name].Pulse ();
			}

		public void Pulse (uint index, int msTimeBetweenTransition)
			{
			BooleanInput[index].Pulse (msTimeBetweenTransition);
			}

		public void Pulse (string name, int msTimeBetweenTransition)
			{
			BooleanInput[name].Pulse (msTimeBetweenTransition);
			}

		public void SetStringValues (IEnumerable<KeyValuePair<uint, string>> values)
			{
			var si = StringInput;

			foreach (var v in values)
				si.SetValue (v.Key, v.Value);
			}

		public void SetStringValues (IEnumerable<KeyValuePair<uint, string>> values, bool repropagate)
			{
			var si = StringInput;

			foreach (var v in values)
				si.SetValue (v.Key, repropagate, v.Value);
			}

		public void SetStringValues (IEnumerable<KeyValuePair<string, string>> values)
			{
			var si = StringInput;

			foreach (var v in values)
				si.SetValue (v.Key, v.Value);
			}

		public void SetStringValues (IEnumerable<KeyValuePair<string, string>> values, bool repropagate)
			{
			var si = StringInput;

			foreach (var v in values)
				si.SetValue (v.Key, v.Value, repropagate);
			}

		public void SetUShortValues (IEnumerable<KeyValuePair<uint, ushort>> values)
			{
			var ii = UShortInput;

			foreach (var v in values)
				ii.SetValue (v.Key, v.Value);
			}

		public void SetUShortValues (IEnumerable<KeyValuePair<uint, ushort>> values, bool repropagate)
			{
			var ii = UShortInput;

			foreach (var v in values)
				ii.SetValue (v.Key, v.Value, repropagate);
			}

		public void SetUShortValues (IEnumerable<KeyValuePair<string, ushort>> values)
			{
			var ii = UShortInput;

			foreach (var v in values)
				ii.SetValue (v.Key, v.Value);
			}

		public void SetUShortValues (IEnumerable<KeyValuePair<string, ushort>> values, bool repropagate)
			{
			var ii = UShortInput;

			foreach (var v in values)
				ii.SetValue (v.Key, v.Value, repropagate);
			}

		public bool GetBooleanInputValue (uint index)
			{
			return BooleanInput[index].BoolValue;
			}

		public bool GetBooleanInputValue (string name)
			{
			return BooleanInput[name].BoolValue;
			}

		public bool GetBooleanOutputValue (uint index)
			{
			return BooleanOutput[index].BoolValue;
			}

		public bool GetBooleanOutputValue (string name)
			{
			return BooleanOutput[name].BoolValue;
			}

		public ushort GetUShortInputValue (uint index)
			{
			return UShortInput[index].UShortValue;
			}

		public ushort GetUShortInputValue (string name)
			{
			return UShortInput[name].UShortValue;
			}

		public ushort GetUShortOutputValue (uint index)
			{
			return UShortOutput[index].UShortValue;
			}

		public ushort GetUShortOutputValue (string name)
			{
			return UShortOutput[name].UShortValue;
			}

		public string GetStringInputValue (uint index)
			{
			return StringInput[index].StringValue;
			}

		public string GetStringInputValue (string name)
			{
			return StringInput[name].StringValue;
			}
		public string GetStringOutputValue (uint index)
			{
			return StringOutput[index].StringValue;
			}

		public string GetStringOutputValue (string name)
			{
			return StringOutput[name].StringValue;
			}

		public void CreateRamp (uint index, ushort finalRampValue, uint timeToRampIn10msIntervals)
			{
			UShortInput[index].CreateRamp (finalRampValue, timeToRampIn10msIntervals);
			}

		public void CreateRamp (string name, ushort finalRampValue, uint timeToRampIn10msIntervals)
			{
			UShortInput[name].CreateRamp (finalRampValue, timeToRampIn10msIntervals);
			}

		public void StopRamp (uint index)
			{
			UShortInput[index].StopRamp ();
			}

		public void StopRamp (string name)
			{
			UShortInput[name].StopRamp ();
			}

		public void CreateRampWithEvent (uint index, ushort finalRampValue, uint timeToRampIn10msIntervals, RampChangeHandler changeHandler)
			{
			UShortInput.CreateRampWithEvent (index, finalRampValue, timeToRampIn10msIntervals, changeHandler);
			}

		public void CreateRampWithEvent (string name, ushort finalRampValue, uint timeToRampIn10msIntervals, RampChangeHandler changeHandler)
			{
			UShortInput.CreateRampWithEvent (name, finalRampValue, timeToRampIn10msIntervals, changeHandler);
			}
		}

	internal class UnifiedSigEventInfo : IEquatable<UnifiedSigEventInfo>
		{
		public UnifiedSigEventHandler SigChange;
		public eSigEvent eSigEvent;


		#region IEquatable<UnifiedCueEventInfo> Members

		public bool Equals (UnifiedSigEventInfo other)
			{
			return eSigEvent == other.eSigEvent && SigChange == other.SigChange;
			}

		#endregion
		}

	internal class UnifiedSigUserObject
		{
		public object UserObject;
		public List<UnifiedSigEventInfo> UnifiedSigEvents;
		}

	public static class SigExtensions
		{
		public static object GetUserObject (this Sig cue)
			{
			var uuo = cue.UserObject as UnifiedSigUserObject;
			return uuo == null ? cue.UserObject : uuo.UserObject;
			}

		public static object GetUserObject<T> (this SigCollectionBase<T> coll, uint index) where T : Sig
			{
			return coll[index].GetUserObject ();
			}

		public static object GetUserObject<T> (this SigCollectionBase<T> coll, string name) where T : Sig
			{
			return coll[name].GetUserObject ();
			}

		public static void SetUserObject (this Sig cue, object value)
			{
			var uuo = cue.UserObject as UnifiedSigUserObject;
			if (uuo == null)
				cue.UserObject = value;
			else
				uuo.UserObject = value;
			}

		public static void SetUserObject<T> (this SigCollectionBase<T> coll, uint index, object value) where T : Sig
			{
			coll[index].SetUserObject (value);
			}

		public static void SetUserObject<T> (this SigCollectionBase<T> coll, string name, object value) where T : Sig
			{
			coll[name].SetUserObject (value);
			}

		public static void AttachSigEventHandler (this Sig cue, UnifiedSigEventHandler handler)
			{
			eSigEvent eventType;
			switch (cue.Type)
				{
				case eSigType.Bool:
					eventType = eSigEvent.BoolChange;
					break;
				case eSigType.UShort:
					eventType = cue.IsInput ? eSigEvent.UShortInputRamping : eSigEvent.UShortChange;
					break;
				case eSigType.String:
					eventType = eSigEvent.StringChange;
					break;
				default:
					eventType = eSigEvent.NA;
					break;
				}

			cue.AttachSigEventHandler (handler, eventType);
			}

		public static void AttachSigEventHandler (this BoolOutputSig cue, UnifiedSigEventHandler handler)
			{
			cue.AttachSigEventHandler (handler, eSigEvent.BoolChange);
			}

		public static void AttachSigEventHandler (this UShortOutputSig cue, UnifiedSigEventHandler handler)
			{
			cue.AttachSigEventHandler (handler, eSigEvent.UShortChange);
			}

		public static void AttachSigEventHandler (this StringOutputSig cue, UnifiedSigEventHandler handler)
			{
			cue.AttachSigEventHandler (handler, eSigEvent.StringChange);
			}

		public static void AttachSigEventHandler (this UShortInputSig cue, UnifiedSigEventHandler handler)
			{
			cue.AttachSigEventHandler (handler, eSigEvent.UShortInputRamping);
			}

		public static void AttachSigEventHandler (this Sig cue, UnifiedSigEventHandler handler, eSigEvent eventType)
			{
			var uuo = cue.UserObject as UnifiedSigUserObject;
			if (uuo == null)
				{
				uuo = new UnifiedSigUserObject { UserObject = cue.UserObject };
				cue.UserObject = uuo;
				}
			if (uuo.UnifiedSigEvents == null)
				uuo.UnifiedSigEvents = new List<UnifiedSigEventInfo> ();

			var ucei = new UnifiedSigEventInfo { eSigEvent = eventType, SigChange = handler };
			uuo.UnifiedSigEvents.Add (ucei);
			}

		public static void AttachSigEventHandler<T> (this SigCollectionBase<T> coll, uint index, UnifiedSigEventHandler handler) where T : Sig
			{
			coll[index].AttachSigEventHandler (handler);
			}

		public static void AttachSigEventHandler<T> (this SigCollectionBase<T> coll, uint index, UnifiedSigEventHandler handler, eSigEvent eventType) where T : Sig
			{
			coll[index].AttachSigEventHandler (handler, eventType);
			}

		public static void AttachSigEventHandler<T> (this SigCollectionBase<T> coll, string name, UnifiedSigEventHandler handler) where T : Sig
			{
			coll[name].AttachSigEventHandler (handler);
			}

		public static void AttachSigEventHandler<T> (this SigCollectionBase<T> coll, string name, UnifiedSigEventHandler handler, eSigEvent eventType) where T : Sig
			{
			coll[name].AttachSigEventHandler (handler, eventType);
			}

		public static void DetachSigEventHandler (this Sig cue, UnifiedSigEventHandler handler)
			{
			eSigEvent eventType;
			switch (cue.Type)
				{
				case eSigType.Bool:
					eventType = eSigEvent.BoolChange;
					break;
				case eSigType.UShort:
					eventType = cue.IsInput ? eSigEvent.UShortInputRamping : eSigEvent.UShortChange;
					break;
				case eSigType.String:
					eventType = eSigEvent.StringChange;
					break;
				default:
					eventType = eSigEvent.NA;
					break;
				}

			cue.DetachSigEventHandler (handler, eventType);
			}

		public static void DetachSigEventHandler (this BoolOutputSig cue, UnifiedSigEventHandler handler)
			{
			cue.DetachSigEventHandler (handler, eSigEvent.BoolChange);
			}

		public static void DetachSigEventHandler (this UShortOutputSig cue, UnifiedSigEventHandler handler)
			{
			cue.DetachSigEventHandler (handler, eSigEvent.UShortChange);
			}

		public static void DetachSigEventHandler (this StringOutputSig cue, UnifiedSigEventHandler handler)
			{
			cue.DetachSigEventHandler (handler, eSigEvent.StringChange);
			}

		public static void DetachSigEventHandler (this UShortInputSig cue, UnifiedSigEventHandler handler)
			{
			cue.DetachSigEventHandler (handler, eSigEvent.UShortInputRamping);
			}

		public static void DetachSigEventHandler (this Sig cue, UnifiedSigEventHandler handler, eSigEvent eventType)
			{
			var uuo = cue.UserObject as UnifiedSigUserObject;
			if (uuo == null || uuo.UnifiedSigEvents == null)
				return;

			var ucei = uuo.UnifiedSigEvents.FirstOrDefault (u => u.eSigEvent == eventType && u.SigChange == handler);
			if (ucei == null)
				return;

			uuo.UnifiedSigEvents.Remove (ucei);

			if (uuo.UnifiedSigEvents.Count == 0)
				cue.UserObject = uuo.UserObject;
			}

		public static void DetachSigEventHandler<T> (this SigCollectionBase<T> coll, uint index, UnifiedSigEventHandler handler) where T : Sig
			{
			coll[index].DetachSigEventHandler (handler);
			}

		public static void DetachSigEventHandler<T> (this SigCollectionBase<T> coll, uint index, UnifiedSigEventHandler handler, eSigEvent eventType) where T : Sig
			{
			coll[index].DetachSigEventHandler (handler, eventType);
			}

		public static void DetachSigEventHandler<T> (this SigCollectionBase<T> coll, string name, UnifiedSigEventHandler handler) where T : Sig
			{
			coll[name].DetachSigEventHandler (handler);
			}

		public static void DetachSigEventHandler<T> (this SigCollectionBase<T> coll, string name, UnifiedSigEventHandler handler, eSigEvent eventType) where T : Sig
			{
			coll[name].DetachSigEventHandler (handler, eventType);
			}

		public static void DetachAllSigEventHandlers (this Sig cue)
			{
			var uuo = cue.UserObject as UnifiedSigUserObject;
			if (uuo == null)
				return;

			cue.UserObject = uuo.UserObject;
			}

		public static void DetachAllSigEventHandlers<T> (this SigCollectionBase<T> coll, uint index) where T : Sig
			{
			coll[index].DetachAllSigEventHandlers ();
			}

		public static void DetachAllSigEventHandlers<T> (this SigCollectionBase<T> coll, string name) where T : Sig
			{
			coll[name].DetachAllSigEventHandlers ();
			}
		}

	public class UnifiedSigEventArgs : EventArgs
		{
		public eSigEvent Event { get; private set; }
		public Sig Sig { get; private set; }
		public bool BoolValue { get; internal set; }
		public ushort UShortValue { get; internal set; }
		public string StringValue { get; internal set; }

		public UnifiedSigEventArgs (eSigEvent eSigEvent, Sig sig)
			{
			Event = eSigEvent;
			Sig = sig;
			}

		public UnifiedSigEventArgs (eSigEvent eSigEvent, Sig sig, bool value)
			: this (eSigEvent, sig)
			{
			BoolValue = value;
			}

		public UnifiedSigEventArgs (eSigEvent eSigEvent, Sig sig, ushort value)
			: this (eSigEvent, sig)
			{
			UShortValue = value;
			}

		public UnifiedSigEventArgs (eSigEvent eSigEvent, Sig sig, string value)
			: this (eSigEvent, sig)
			{
			StringValue = value;
			}

		public UnifiedSigEventArgs (SigEventArgs args)
			: this (args.Event, args.Sig)
			{

			}
		}
	}