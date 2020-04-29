#region Copyright and License
// -----------------------------------------------------------------------------------------------------------------
// 
// DeviceExtension.cs
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
using Crestron.SimplSharp;
using Crestron.SimplSharpPro.DeviceSupport;
using GC = Crestron.SimplSharp.CrestronEnvironment.GC;

namespace Crestron.SimplSharpPro.Helpers
	{
	public delegate void RampChangeHandler (UShortInputSig sig, ushort value);

	public static class DeviceExtension
		{
		private const int MAXIMUMSTRINGLENGTH = 1024;

		public static void SetValue (this StringInputSig sig, string value)
			{
			sig.SetValue (false, value);
			}

		public static void SetValue (this StringInputSig sig, string format, params object[] args)
			{
			sig.SetValue (false, format, args);
			}

		public static void SetValue (this StringInputSig sig, bool repropagate, string value)
			{
			//if (value == null || value.Length <= MAXIMUMSTRINGLENGTH)
				{
				if (!repropagate && sig.StringValue == value)
					return;

				sig.StringValue = value;
				return;
				}

			/*
			sig.UpdateStringWithOptions (value.Substring (0, MAXIMUMSTRINGLENGTH), eInboundTextModes.e_InboundTextModeClear, eStringEncoding.eEncodingASCII);
			int ix;
			for (ix = MAXIMUMSTRINGLENGTH; ix < value.Length - MAXIMUMSTRINGLENGTH; ix += MAXIMUMSTRINGLENGTH)
				sig.UpdateStringWithOptions (value.Substring (ix, MAXIMUMSTRINGLENGTH), eInboundTextModes.e_InboundTextModeAppendToBuffer, eStringEncoding.eEncodingASCII);
			sig.UpdateStringWithOptions (value.Substring (ix, value.Length - ix), eInboundTextModes.e_InboundTextModeEnd, eStringEncoding.eEncodingASCII);
			*/
			}

		public static void SetValue (this StringInputSig sig, bool repropagate, string format, params object[] args)
			{
			sig.SetValue (repropagate, String.Format (format, args));
			}

		public static void SetValue (this DeviceStringInputCollection inputs, uint index, string value)
			{
			inputs[index].SetValue (value);
			}

		public static void SetValue (this DeviceStringInputCollection inputs, uint index, string format, params object[] args)
			{
			inputs[index].SetValue (format, args);
			}

		public static void SetValue (this DeviceStringInputCollection inputs, uint index, bool repropagate, string value)
			{
			inputs[index].SetValue (repropagate, value);
			}

		public static void SetValue (this DeviceStringInputCollection inputs, uint index, bool repropagate, string format, params object[] args)
			{
			inputs[index].SetValue (repropagate, format, args);
			}

		public static void SetValue (this DeviceStringInputCollection inputs, string name, string value)
			{
			inputs[name].SetValue (value);
			}

		public static void SetValue (this DeviceStringInputCollection inputs, string name, string format, params object[] args)
			{
			inputs[name].SetValue (format, args);
			}

		public static void SetValue (this DeviceStringInputCollection inputs, string name, bool repropagate, string value)
			{
			inputs[name].SetValue (repropagate, value);
			}

		public static void SetValue (this DeviceStringInputCollection inputs, string name, bool repropagate, string format, params object[] args)
			{
			inputs[name].SetValue (repropagate, format, args);
			}

		public static void SetValue (this UShortInputSig sig, ushort value)
			{
			sig.SetValue (value, false);
			}

		public static void SetValue (this UShortInputSig sig, ushort value, bool repropagate)
			{
			if (!repropagate && sig.UShortValue == value)
				return;

			sig.UShortValue = value;
			}

		public static void SetValue (this DeviceUShortInputCollection inputs, uint index, ushort value)
			{
			inputs[index].SetValue (value);
			}

		public static void SetValue (this DeviceUShortInputCollection inputs, uint index, ushort value, bool repropagate)
			{
			inputs[index].SetValue (value, repropagate);
			}

		public static void SetValue (this DeviceUShortInputCollection inputs, string name, ushort value)
			{
			inputs[name].SetValue (value);
			}

		public static void SetValue (this DeviceUShortInputCollection inputs, string name, ushort value, bool repropagate)
			{
			inputs[name].SetValue (value, repropagate);
			}

		public static void SetValue (this BoolInputSig sig, bool value)
			{
			sig.BoolValue = value;
			}

		public static void SetTrue (this BoolInputSig sig)
			{
			sig.BoolValue = true;
			}

		public static void SetFalse (this BoolInputSig sig)
			{
			sig.BoolValue = false;
			}

		public static void SetValue (this DeviceBooleanInputCollection inputs, uint index, bool value)
			{
			inputs[index].BoolValue = value;
			}

		public static void SetValue (this DeviceBooleanInputCollection inputs, string name, bool value)
			{
			inputs[name].BoolValue = value;
			}

		public static void SetTrue (this DeviceBooleanInputCollection inputs, uint index)
			{
			inputs[index].BoolValue = true;
			}

		public static void SetTrue (this DeviceBooleanInputCollection inputs, string name)
			{
			inputs[name].BoolValue = true;
			}

		public static void SetFalse (this DeviceBooleanInputCollection inputs, uint index)
			{
			inputs[index].BoolValue = false;
			}

		public static void SetFalse (this DeviceBooleanInputCollection inputs, string name)
			{
			inputs[name].BoolValue = false;
			}

		public static void Pulse (this DeviceBooleanInputCollection inputs, uint index)
			{
			inputs[index].Pulse ();
			}

		public static void Pulse (this DeviceBooleanInputCollection inputs, string name)
			{
			inputs[name].Pulse ();
			}

		public static void Pulse (this DeviceBooleanInputCollection inputs, uint index, int msTimeBetweenTransition)
			{
			inputs[index].Pulse (msTimeBetweenTransition);
			}

		public static void Pulse (this DeviceBooleanInputCollection inputs, string name, int msTimeBetweenTransition)
			{
			inputs[name].Pulse (msTimeBetweenTransition);
			}

		public static void SetValue (this BasicTriList device, uint index, string value)
			{
			device.SetStringValue (index, value);
			}

		public static void SetValue (this BasicTriList device, string name, string value)
			{
			device.SetStringValue (name, value);
			}

		public static void SetValue (this BasicTriList device, uint index, ushort value)
			{
			device.SetUShortValue (index, value);
			}

		public static void SetValue (this BasicTriList device, string name, ushort value)
			{
			device.SetUShortValue (name, value);
			}

		public static void SetValue (this BasicTriList device, uint index, bool value)
			{
			device.SetBoolValue (index, value);
			}

		public static void SetValue (this BasicTriList device, string name, bool value)
			{
			device.SetBoolValue (name, value);
			}

		public static void SetStringValue (this BasicTriList device, uint index, string value)
			{
			device.StringInput.SetValue (index, value);
			}

		public static void SetStringValue (this BasicTriList device, uint index, bool repropagate, string value)
			{
			device.StringInput.SetValue (index, repropagate, value);
			}

		public static void SetStringValue (this BasicTriList device, string name, string value)
			{
			device.StringInput.SetValue (name, value);
			}

		public static void SetStringValue (this BasicTriList device, string name, string format, params object[] args)
			{
			device.StringInput.SetValue (name, format, args);
			}

		public static void SetStringValue (this BasicTriList device, string name, bool repropagate, string value)
			{
			device.StringInput.SetValue (name, repropagate, value);
			}
		public static void SetStringValue (this BasicTriList device, string name, bool repropagate, string format, params object[] args)
			{
			device.StringInput.SetValue (name, repropagate, format, args);
			}

		public static void SetUShortValue (this BasicTriList device, uint index, ushort value)
			{
			device.UShortInput.SetValue (index, value);
			}

		public static void SetUShortValue (this BasicTriList device, uint index, ushort value, bool repropagate)
			{
			device.UShortInput.SetValue (index, value, repropagate);
			}

		public static void SetUShortValue (this BasicTriList device, string name, ushort value)
			{
			device.UShortInput.SetValue (name, value);
			}

		public static void SetUShortValue (this BasicTriList device, string name, ushort value, bool repropagate)
			{
			device.UShortInput.SetValue (name, value, repropagate);
			}

		public static void SetBoolValue (this BasicTriList device, uint index, bool value)
			{
			device.BooleanInput[index].BoolValue = value;
			}

		public static void SetBoolValue (this BasicTriList device, string name, bool value)
			{
			device.BooleanInput[name].BoolValue = value;
			}

		public static void SetTrue (this BasicTriList device, uint index)
			{
			device.BooleanInput[index].BoolValue = true;
			}

		public static void SetTrue (this BasicTriList device, string name)
			{
			device.BooleanInput[name].BoolValue = true;
			}

		public static void SetFalse (this BasicTriList device, uint index)
			{
			device.BooleanInput[index].BoolValue = false;
			}

		public static void SetFalse (this BasicTriList device, string name)
			{
			device.BooleanInput[name].BoolValue = false;
			}

		public static void Pulse (this BasicTriList device, uint index)
			{
			device.BooleanInput[index].Pulse ();
			}

		public static void Pulse (this BasicTriList device, string name)
			{
			device.BooleanInput[name].Pulse ();
			}

		public static void Pulse (this BasicTriList device, uint index, int msTimeBetweenTransition)
			{
			device.BooleanInput[index].Pulse (msTimeBetweenTransition);
			}

		public static void Pulse (this BasicTriList device, string name, int msTimeBetweenTransition)
			{
			device.BooleanInput[name].Pulse (msTimeBetweenTransition);
			}

		public static void SetStringValues (this BasicTriList device, IEnumerable<KeyValuePair<uint, string>> values)
			{
			var si = device.StringInput;

			foreach (var v in values)
				si.SetValue (v.Key, v.Value);
			}

		public static void SetStringValues (this BasicTriList device, IEnumerable<KeyValuePair<uint, string>> values, bool repropagate)
			{
			var si = device.StringInput;

			foreach (var v in values)
				si.SetValue (v.Key, repropagate, v.Value);
			}

		public static void SetStringValues (this BasicTriList device, IEnumerable<KeyValuePair<string, string>> values)
			{
			var si = device.StringInput;

			foreach (var v in values)
				si.SetValue (v.Key, v.Value);
			}

		public static void SetStringValues (this BasicTriList device, IEnumerable<KeyValuePair<string, string>> values, bool repropagate)
			{
			var si = device.StringInput;

			foreach (var v in values)
				si.SetValue (v.Key, v.Value, repropagate);
			}

		public static void SetUShortValues (this BasicTriList device, IEnumerable<KeyValuePair<uint, ushort>> values)
			{
			var ii = device.UShortInput;

			foreach (var v in values)
				ii.SetValue (v.Key, v.Value);
			}

		public static void SetUShortValues (this BasicTriList device, IEnumerable<KeyValuePair<uint, ushort>> values, bool repropagate)
			{
			var ii = device.UShortInput;

			foreach (var v in values)
				ii.SetValue (v.Key, v.Value, repropagate);
			}

		public static void SetUShortValues (this BasicTriList device, IEnumerable<KeyValuePair<string, ushort>> values)
			{
			var ii = device.UShortInput;

			foreach (var v in values)
				ii.SetValue (v.Key, v.Value);
			}

		public static void SetUShortValues (this BasicTriList device, IEnumerable<KeyValuePair<string, ushort>> values, bool repropagate)
			{
			var ii = device.UShortInput;

			foreach (var v in values)
				ii.SetValue (v.Key, v.Value, repropagate);
			}

		public static void SetValue (this SmartObject smartObject, uint index, string value)
			{
			smartObject.SetStringValue (index, value);
			}

		public static void SetValue (this SmartObject smartObject, string name, string value)
			{
			smartObject.SetStringValue (name, value);
			}

		public static void SetValue (this SmartObject smartObject, uint index, string format, params object[] args)
			{
			smartObject.SetStringValue (index, format, args);
			}

		public static void SetValue (this SmartObject smartObject, string name, string format, params object[] args)
			{
			smartObject.SetStringValue (name, format, args);
			}

		public static void SetValue (this SmartObject smartObject, uint index, ushort value)
			{
			smartObject.SetUShortValue (index, value);
			}
		public static void SetValue (this SmartObject smartObject, string name, ushort value)
			{
			smartObject.SetUShortValue (name, value);
			}

		public static void SetValue (this SmartObject smartObject, uint index, bool value)
			{
			smartObject.SetBoolValue (index, value);
			}

		public static void SetValue (this SmartObject smartObject, string name, bool value)
			{
			smartObject.SetBoolValue (name, value);
			}

		public static void SetStringValue (this SmartObject smartObject, uint index, string value)
			{
			smartObject.StringInput.SetValue (index, value);
			}

		public static void SetStringValue (this SmartObject smartObject, uint index, bool repropagate, string value)
			{
			smartObject.StringInput.SetValue (index, repropagate, value);
			}

		public static void SetStringValue (this SmartObject smartObject, string name, string value)
			{
			smartObject.StringInput.SetValue (name, value);
			}

		public static void SetStringValue (this SmartObject smartObject, string name, bool repropagate, string value)
			{
			smartObject.StringInput.SetValue (name, repropagate, value);
			}

		public static void SetStringValue (this SmartObject smartObject, uint index, string format, params object[] args)
			{
			smartObject.StringInput.SetValue (index, format, args);
			}

		public static void SetStringValue (this SmartObject smartObject, uint index, bool repropagate, string format, params object[] args)
			{
			smartObject.StringInput.SetValue (index, repropagate, format, args);
			}

		public static void SetStringValue (this SmartObject smartObject, string name, string format, params object[] args)
			{
			smartObject.StringInput.SetValue (name, format, args);
			}

		public static void SetStringValue (this SmartObject smartObject, string name, bool repropagate, string format, params object[] args)
			{
			smartObject.StringInput.SetValue (name, repropagate, format, args);
			}

		public static void SetUShortValue (this SmartObject smartObject, uint index, ushort value)
			{
			smartObject.UShortInput.SetValue (index, value);
			}

		public static void SetUShortValue (this SmartObject smartObject, uint index, ushort value, bool repropagate)
			{
			smartObject.UShortInput.SetValue (index, value, repropagate);
			}

		public static void SetUShortValue (this SmartObject smartObject, string name, ushort value)
			{
			smartObject.UShortInput.SetValue (name, value);
			}

		public static void SetUShortValue (this SmartObject smartObject, string name, ushort value, bool repropagate)
			{
			smartObject.UShortInput.SetValue (name, value, repropagate);
			}

		public static void SetBoolValue (this SmartObject smartObject, uint index, bool value)
			{
			smartObject.BooleanInput[index].BoolValue = value;
			}

		public static void SetBoolValue (this SmartObject smartObject, string name, bool value)
			{
			smartObject.BooleanInput[name].BoolValue = value;
			}

		public static void SetTrue (this SmartObject smartObject, uint index)
			{
			smartObject.BooleanInput[index].BoolValue = true;
			}

		public static void SetTrue (this SmartObject smartObject, string name)
			{
			smartObject.BooleanInput[name].BoolValue = true;
			}

		public static void SetFalse (this SmartObject smartObject, uint index)
			{
			smartObject.BooleanInput[index].BoolValue = false;
			}

		public static void SetFalse (this SmartObject smartObject, string name)
			{
			smartObject.BooleanInput[name].BoolValue = false;
			}

		public static void Pulse (this SmartObject smartObject, uint index)
			{
			smartObject.BooleanInput[index].Pulse ();
			}

		public static void Pulse (this SmartObject smartObject, string name)
			{
			smartObject.BooleanInput[name].Pulse ();
			}

		public static void Pulse (this SmartObject smartObject, uint index, int msTimeBetweenTransition)
			{
			smartObject.BooleanInput[index].Pulse (msTimeBetweenTransition);
			}

		public static void Pulse (this SmartObject smartObject, string name, int msTimeBetweenTransition)
			{
			smartObject.BooleanInput[name].Pulse (msTimeBetweenTransition);
			}

		public static void SetStringValues (this SmartObject smartObject, IEnumerable<KeyValuePair<uint, string>> values)
			{
			var si = smartObject.StringInput;

			foreach (var v in values)
				si.SetValue (v.Key, v.Value);
			}

		public static void SetStringValues (this SmartObject smartObject, IEnumerable<KeyValuePair<uint, string>> values, bool repropagate)
			{
			var si = smartObject.StringInput;

			foreach (var v in values)
				si.SetValue (v.Key, repropagate, v.Value);
			}

		public static void SetStringValues (this SmartObject smartObject, IEnumerable<KeyValuePair<string, string>> values)
			{
			var si = smartObject.StringInput;

			foreach (var v in values)
				si.SetValue (v.Key, v.Value);
			}

		public static void SetStringValues (this SmartObject smartObject, IEnumerable<KeyValuePair<string, string>> values, bool repropagate)
			{
			var si = smartObject.StringInput;

			foreach (var v in values)
				si.SetValue (v.Key, repropagate, v.Value);
			}

		public static void SetUShortValues (this SmartObject smartObject, IEnumerable<KeyValuePair<uint, ushort>> values)
			{
			var ii = smartObject.UShortInput;

			foreach (var v in values)
				ii.SetValue (v.Key, v.Value);
			}

		public static void SetUShortValues (this SmartObject smartObject, IEnumerable<KeyValuePair<uint, ushort>> values, bool repropagate)
			{
			var ii = smartObject.UShortInput;

			foreach (var v in values)
				ii.SetValue (v.Key, v.Value, repropagate);
			}

		public static void SetUShortValues (this SmartObject smartObject, IEnumerable<KeyValuePair<string, ushort>> values)
			{
			var ii = smartObject.UShortInput;

			foreach (var v in values)
				ii.SetValue (v.Key, v.Value);
			}

		public static void SetUShortValues (this SmartObject smartObject, IEnumerable<KeyValuePair<string, ushort>> values, bool repropagate)
			{
			var ii = smartObject.UShortInput;

			foreach (var v in values)
				ii.SetValue (v.Key, v.Value, repropagate);
			}

		public static void SetValue (this CrestronDeviceWithEvents device, uint index, string value)
			{
			device.SetStringValue (index, value);
			}

		public static void SetValue (this CrestronDeviceWithEvents device, string name, string value)
			{
			device.SetStringValue (name, value);
			}

		public static void SetValue (this CrestronDeviceWithEvents device, uint index, string format, params object[] args)
			{
			device.SetStringValue (index, format, args);
			}

		public static void SetValue (this CrestronDeviceWithEvents device, string name, string format, params object[] args)
			{
			device.SetStringValue (name, format, args);
			}

		public static void SetValue (this CrestronDeviceWithEvents device, uint index, ushort value)
			{
			device.SetUShortValue (index, value);
			}

		public static void SetValue (this CrestronDeviceWithEvents device, string name, ushort value)
			{
			device.SetUShortValue (name, value);
			}

		public static void SetValue (this CrestronDeviceWithEvents device, uint index, bool value)
			{
			device.SetBoolValue (index, value);
			}

		public static void SetValue (this CrestronDeviceWithEvents device, string name, bool value)
			{
			device.SetBoolValue (name, value);
			}

		public static void SetStringValue (this CrestronDeviceWithEvents device, uint index, string value)
			{
			device.StringInput.SetValue (index, value);
			}

		public static void SetStringValue (this CrestronDeviceWithEvents device, uint index, bool repropagate, string value)
			{
			device.StringInput.SetValue (index, repropagate, value);
			}

		public static void SetStringValue (this CrestronDeviceWithEvents device, string name, string value)
			{
			device.StringInput.SetValue (name, value);
			}

		public static void SetStringValue (this CrestronDeviceWithEvents device, string name, bool repropagate, string value)
			{
			device.StringInput.SetValue (name, repropagate, value);
			}

		public static void SetStringValue (this CrestronDeviceWithEvents device, uint index, string format, params object[] args)
			{
			device.StringInput.SetValue (index, format, args);
			}

		public static void SetStringValue (this CrestronDeviceWithEvents device, uint index, bool repropagate, string format, params object[] args)
			{
			device.StringInput.SetValue (index, repropagate, format, args);
			}

		public static void SetStringValue (this CrestronDeviceWithEvents device, string name, string format, params object[] args)
			{
			device.StringInput.SetValue (name, format, args);
			}

		public static void SetStringValue (this CrestronDeviceWithEvents device, string name, bool repropagate, string format, params object[] args)
			{
			device.StringInput.SetValue (name, repropagate, format, args);
			}

		public static void SetUShortValue (this CrestronDeviceWithEvents device, uint index, ushort value)
			{
			device.UShortInput.SetValue (index, value);
			}

		public static void SetUShortValue (this CrestronDeviceWithEvents device, uint index, ushort value, bool repropagate)
			{
			device.UShortInput.SetValue (index, value, repropagate);
			}

		public static void SetUShortValue (this CrestronDeviceWithEvents device, string name, ushort value)
			{
			device.UShortInput.SetValue (name, value);
			}

		public static void SetUShortValue (this CrestronDeviceWithEvents device, string name, ushort value, bool repropagate)
			{
			device.UShortInput.SetValue (name, value, repropagate);
			}

		public static void SetBoolValue (this CrestronDeviceWithEvents device, uint index, bool value)
			{
			device.BooleanInput[index].BoolValue = value;
			}

		public static void SetBoolValue (this CrestronDeviceWithEvents device, string name, bool value)
			{
			device.BooleanInput[name].BoolValue = value;
			}

		public static void SetTrue (this CrestronDeviceWithEvents device, uint index)
			{
			device.BooleanInput[index].BoolValue = true;
			}

		public static void SetTrue (this CrestronDeviceWithEvents device, string name)
			{
			device.BooleanInput[name].BoolValue = true;
			}

		public static void SetFalse (this CrestronDeviceWithEvents device, uint index)
			{
			device.BooleanInput[index].BoolValue = false;
			}

		public static void SetFalse (this CrestronDeviceWithEvents device, string name)
			{
			device.BooleanInput[name].BoolValue = false;
			}

		public static void Pulse (this CrestronDeviceWithEvents device, uint index)
			{
			device.BooleanInput[index].Pulse ();
			}

		public static void Pulse (this CrestronDeviceWithEvents device, string name)
			{
			device.BooleanInput[name].Pulse ();
			}

		public static void Pulse (this CrestronDeviceWithEvents device, uint index, int msTimeBetweenTransition)
			{
			device.BooleanInput[index].Pulse (msTimeBetweenTransition);
			}

		public static void Pulse (this CrestronDeviceWithEvents device, string name, int msTimeBetweenTransition)
			{
			device.BooleanInput[name].Pulse (msTimeBetweenTransition);
			}

		public static void SetStringValues (this CrestronDeviceWithEvents device, IEnumerable<KeyValuePair<uint, string>> values)
			{
			var si = device.StringInput;

			foreach (var v in values)
				si.SetValue (v.Key, v.Value);
			}

		public static void SetStringValues (this CrestronDeviceWithEvents device, IEnumerable<KeyValuePair<uint, string>> values, bool repropagate)
			{
			var si = device.StringInput;

			foreach (var v in values)
				si.SetValue (v.Key, repropagate, v.Value);
			}

		public static void SetStringValues (this CrestronDeviceWithEvents device, IEnumerable<KeyValuePair<string, string>> values)
			{
			var si = device.StringInput;

			foreach (var v in values)
				si.SetValue (v.Key, v.Value);
			}

		public static void SetStringValues (this CrestronDeviceWithEvents device, IEnumerable<KeyValuePair<string, string>> values, bool repropagate)
			{
			var si = device.StringInput;

			foreach (var v in values)
				si.SetValue (v.Key, repropagate, v.Value);
			}

		public static void SetUShortValues (this CrestronDeviceWithEvents device, IEnumerable<KeyValuePair<uint, ushort>> values)
			{
			var ii = device.UShortInput;

			foreach (var v in values)
				ii.SetValue (v.Key, v.Value);
			}

		public static void SetUShortValues (this CrestronDeviceWithEvents device, IEnumerable<KeyValuePair<uint, ushort>> values, bool repropagate)
			{
			var ii = device.UShortInput;

			foreach (var v in values)
				ii.SetValue (v.Key, v.Value, repropagate);
			}

		public static void SetUShortValues (this CrestronDeviceWithEvents device, IEnumerable<KeyValuePair<string, ushort>> values)
			{
			var ii = device.UShortInput;

			foreach (var v in values)
				ii.SetValue (v.Key, v.Value);
			}

		public static void SetUShortValues (this CrestronDeviceWithEvents device, IEnumerable<KeyValuePair<string, ushort>> values, bool repropagate)
			{
			var ii = device.UShortInput;

			foreach (var v in values)
				ii.SetValue (v.Key, v.Value, repropagate);
			}

		public static bool GetBoolValue (this DeviceBooleanInputCollection inputs, uint index)
			{
			return inputs[index].BoolValue;
			}

		public static bool GetBoolValue (this DeviceBooleanInputCollection inputs, string name)
			{
			return inputs[name].BoolValue;
			}

		public static bool GetBoolValue (this DeviceBooleanOutputCollection outputs, uint index)
			{
			return outputs[index].BoolValue;
			}

		public static bool GetBoolValue (this DeviceBooleanOutputCollection outputs, string name)
			{
			return outputs[name].BoolValue;
			}

		public static ushort GetUShortValue (this DeviceUShortInputCollection inputs, uint index)
			{
			return inputs[index].UShortValue;
			}

		public static ushort GetUShortValue (this DeviceUShortInputCollection inputs, string name)
			{
			return inputs[name].UShortValue;
			}

		public static ushort GetUShortValue (this DeviceUShortOutputCollection outputs, uint index)
			{
			return outputs[index].UShortValue;
			}

		public static ushort GetUShortValue (this DeviceUShortOutputCollection outputs, string name)
			{
			return outputs[name].UShortValue;
			}

		public static string GetStringValue (this DeviceStringInputCollection inputs, uint index)
			{
			return inputs[index].StringValue;
			}

		public static string GetStringValue (this DeviceStringInputCollection inputs, string name)
			{
			return inputs[name].StringValue;
			}

		public static string GetStringValue (this DeviceStringOutputCollection outputs, uint index)
			{
			return outputs[index].StringValue;
			}

		public static string GetStringValue (this DeviceStringOutputCollection outputs, string name)
			{
			return outputs[name].StringValue;
			}

		public static bool GetBooleanInputValue (this CrestronDeviceWithEvents device, uint index)
			{
			return device.BooleanInput[index].BoolValue;
			}

		public static bool GetBooleanInputValue (this CrestronDeviceWithEvents device, string name)
			{
			return device.BooleanInput[name].BoolValue;
			}

		public static bool GetBooleanOutputValue (this CrestronDeviceWithEvents device, uint index)
			{
			return device.BooleanOutput[index].BoolValue;
			}

		public static bool GetBooleanOutputValue (this CrestronDeviceWithEvents device, string name)
			{
			return device.BooleanOutput[name].BoolValue;
			}

		public static ushort GetUShortInputValue (this CrestronDeviceWithEvents device, uint index)
			{
			return device.UShortInput[index].UShortValue;
			}

		public static ushort GetUShortInputValue (this CrestronDeviceWithEvents device, string name)
			{
			return device.UShortInput[name].UShortValue;
			}

		public static ushort GetUShortOutputValue (this CrestronDeviceWithEvents device, uint index)
			{
			return device.UShortOutput[index].UShortValue;
			}

		public static ushort GetUShortOutputValue (this CrestronDeviceWithEvents device, string name)
			{
			return device.UShortOutput[name].UShortValue;
			}

		public static string GetStringInputValue (this CrestronDeviceWithEvents device, uint index)
			{
			return device.StringInput[index].StringValue;
			}

		public static string GetStringInputValue (this CrestronDeviceWithEvents device, string name)
			{
			return device.StringInput[name].StringValue;
			}

		public static string GetStringOutputValue (this CrestronDeviceWithEvents device, uint index)
			{
			return device.StringOutput[index].StringValue;
			}

		public static string GetStringOutputValue (this CrestronDeviceWithEvents device, string name)
			{
			return device.StringOutput[name].StringValue;
			}

		public static bool GetBooleanInputValue (this SmartObject smartObject, uint index)
			{
			return smartObject.BooleanInput[index].BoolValue;
			}

		public static bool GetBooleanInputValue (this SmartObject smartObject, string name)
			{
			return smartObject.BooleanInput[name].BoolValue;
			}

		public static bool GetBooleanOutputValue (this SmartObject smartObject, uint index)
			{
			return smartObject.BooleanOutput[index].BoolValue;
			}

		public static bool GetBooleanOutputValue (this SmartObject smartObject, string name)
			{
			return smartObject.BooleanOutput[name].BoolValue;
			}

		public static ushort GetUShortInputValue (this SmartObject smartObject, uint index)
			{
			return smartObject.UShortInput[index].UShortValue;
			}

		public static ushort GetUShortInputValue (this SmartObject smartObject, string name)
			{
			return smartObject.UShortInput[name].UShortValue;
			}

		public static ushort GetUShortOutputValue (this SmartObject smartObject, uint index)
			{
			return smartObject.UShortOutput[index].UShortValue;
			}

		public static ushort GetUShortOutputValue (this SmartObject smartObject, string name)
			{
			return smartObject.UShortOutput[name].UShortValue;
			}

		public static string GetStringInputValue (this SmartObject smartObject, uint index)
			{
			return smartObject.StringInput[index].StringValue;
			}

		public static string GetStringInputValue (this SmartObject smartObject, string name)
			{
			return smartObject.StringInput[name].StringValue;
			}

		public static string GetStringOutputValue (this SmartObject smartObject, uint index)
			{
			return smartObject.StringOutput[index].StringValue;
			}

		public static string GetStringOutputValue (this SmartObject smartObject, string name)
			{
			return smartObject.StringOutput[name].StringValue;
			}

		public static bool GetBooleanInputValue (this BasicTriList device, uint index)
			{
			return device.BooleanInput[index].BoolValue;
			}

		public static bool GetBooleanInputValue (this BasicTriList device, string name)
			{
			return device.BooleanInput[name].BoolValue;
			}

		public static bool GetBooleanOutputValue (this BasicTriList device, uint index)
			{
			return device.BooleanOutput[index].BoolValue;
			}

		public static bool GetBooleanOutputValue (this BasicTriList device, string name)
			{
			return device.BooleanOutput[name].BoolValue;
			}

		public static ushort GetUShortInputValue (this BasicTriList device, uint index)
			{
			return device.UShortInput[index].UShortValue;
			}

		public static ushort GetUShortInputValue (this BasicTriList device, string name)
			{
			return device.UShortInput[name].UShortValue;
			}

		public static ushort GetUShortOutputValue (this BasicTriList device, uint index)
			{
			return device.UShortOutput[index].UShortValue;
			}

		public static ushort GetUShortOutputValue (this BasicTriList device, string name)
			{
			return device.UShortOutput[name].UShortValue;
			}

		public static string GetStringInputValue (this BasicTriList device, uint index)
			{
			return device.StringInput[index].StringValue;
			}

		public static string GetStringInputValue (this BasicTriList device, string name)
			{
			return device.StringInput[name].StringValue;
			}


		private class UserRampObject : IDisposable
			{
			public CTimer timerSigChange;
			public RampChangeHandler rampChangeHandler;
			public UShortInputSig sig;
			private bool disposed;

			#region IDisposable Members

			public void Dispose ()
				{
				Dispose (true);
				GC.SuppressFinalize (this);
				}

			#endregion

			private void Dispose (bool disposing)
				{
				if (!disposed)
					{
					if (disposing)
						{
						if (timerSigChange != null)
							{
							timerSigChange.Stop ();
							timerSigChange = null;
							}
						}
					}

				disposed = true;
				}

			~UserRampObject ()
				{
				Dispose (false);
				}
			}

		public static void CreateRampWithEvent (this UShortInputSig sig, ushort finalRampValue, uint timeToRampIn10msIntervals, RampChangeHandler changeHandler)
			{
			ushort currentValue = sig.UShortValue;
			if (currentValue == finalRampValue)
				return;

			if (changeHandler != null)
				{
				uint timems = timeToRampIn10msIntervals * 10;

				var sigChangeInterval = Math.Max (50, timems / (finalRampValue >= currentValue ? (finalRampValue - currentValue) : (currentValue - finalRampValue)));

				var uro = new UserRampObject
					{
					sig = sig,
					rampChangeHandler = changeHandler
					};

				uro.timerSigChange = new CTimer (RampTimerCallback, uro, sigChangeInterval, sigChangeInterval);
				}

			sig.CreateRamp (finalRampValue, timeToRampIn10msIntervals);
			}

		internal static void RampTimerCallback (object state)
			{
			var uro = (UserRampObject)state;
			var sig = uro.sig;

			if (!sig.IsRamping)
				uro.Dispose ();

			if (uro.rampChangeHandler != null)
				uro.rampChangeHandler (sig, sig.UShortValue);
			}

		public static void CreateRampWithEvent (this DeviceUShortInputCollection inputs, uint index, ushort finalRampValue, uint timeToRampIn10msIntervals, RampChangeHandler changeHandler)
			{
			CreateRampWithEvent (inputs[index], finalRampValue, timeToRampIn10msIntervals, changeHandler);
			}

		public static void CreateRampWithEvent (this DeviceUShortInputCollection inputs, string name, ushort finalRampValue, uint timeToRampIn10msIntervals, RampChangeHandler changeHandler)
			{
			CreateRampWithEvent (inputs[name], finalRampValue, timeToRampIn10msIntervals, changeHandler);
			}

		public static void CreateRampWithEvent (this BasicTriList device, uint index, ushort finalRampValue, uint timeToRampIn10msIntervals, RampChangeHandler changeHandler)
			{
			CreateRampWithEvent (device.UShortInput, index, finalRampValue, timeToRampIn10msIntervals, changeHandler);
			}

		public static void CreateRampWithEvent (this BasicTriList device, string name, ushort finalRampValue, uint timeToRampIn10msIntervals, RampChangeHandler changeHandler)
			{
			CreateRampWithEvent (device.UShortInput, name, finalRampValue, timeToRampIn10msIntervals, changeHandler);
			}

		public static void CreateRampWithEvent (this CrestronDeviceWithEvents device, uint index, ushort finalRampValue, uint timeToRampIn10msIntervals, RampChangeHandler changeHandler)
			{
			CreateRampWithEvent (device.UShortInput, index, finalRampValue, timeToRampIn10msIntervals, changeHandler);
			}

		public static void CreateRampWithEvent (this CrestronDeviceWithEvents device, string name, ushort finalRampValue, uint timeToRampIn10msIntervals, RampChangeHandler changeHandler)
			{
			CreateRampWithEvent (device.UShortInput, name, finalRampValue, timeToRampIn10msIntervals, changeHandler);
			}

		public static void CreateRampWithEvent (this SmartObject smartObject, uint index, ushort finalRampValue, uint timeToRampIn10msIntervals, RampChangeHandler changeHandler)
			{
			CreateRampWithEvent (smartObject.UShortInput, index, finalRampValue, timeToRampIn10msIntervals, changeHandler);
			}

		public static void CreateRampWithEvent (this SmartObject smartObject, string name, ushort finalRampValue, uint timeToRampIn10msIntervals, RampChangeHandler changeHandler)
			{
			CreateRampWithEvent (smartObject.UShortInput, name, finalRampValue, timeToRampIn10msIntervals, changeHandler);
			}

		public static void CreateRamp (this UShortInputSig sig, ushort finalRampValue, uint timeToRampIn10msIntervals)
			{

			sig.CreateRamp (finalRampValue, timeToRampIn10msIntervals);
			}

		public static void CreateRamp (this DeviceUShortInputCollection inputs, uint index, ushort finalRampValue, uint timeToRampIn10msIntervals)
			{
			inputs[index].CreateRamp (finalRampValue, timeToRampIn10msIntervals);
			}

		public static void CreateRamp (this DeviceUShortInputCollection inputs, string name, ushort finalRampValue, uint timeToRampIn10msIntervals)
			{
			inputs[name].CreateRamp (finalRampValue, timeToRampIn10msIntervals);
			}

		public static void CreateRamp (this BasicTriList device, uint index, ushort finalRampValue, uint timeToRampIn10msIntervals)
			{
			device.UShortInput[index].CreateRamp (finalRampValue, timeToRampIn10msIntervals);
			}

		public static void CreateRamp (this BasicTriList device, string name, ushort finalRampValue, uint timeToRampIn10msIntervals)
			{
			device.UShortInput[name].CreateRamp (finalRampValue, timeToRampIn10msIntervals);
			}

		public static void CreateRamp (this CrestronDeviceWithEvents device, uint index, ushort finalRampValue, uint timeToRampIn10msIntervals)
			{
			device.UShortInput[index].CreateRamp (finalRampValue, timeToRampIn10msIntervals);
			}

		public static void CreateRamp (this CrestronDeviceWithEvents device, string name, ushort finalRampValue, uint timeToRampIn10msIntervals)
			{
			device.UShortInput[name].CreateRamp (finalRampValue, timeToRampIn10msIntervals);
			}

		public static void CreateRamp (this SmartObject smartObject, uint index, ushort finalRampValue, uint timeToRampIn10msIntervals)
			{
			smartObject.UShortInput[index].CreateRamp (finalRampValue, timeToRampIn10msIntervals);
			}

		public static void CreateRamp (this SmartObject smartObject, string name, ushort finalRampValue, uint timeToRampIn10msIntervals)
			{
			smartObject.UShortInput[name].CreateRamp (finalRampValue, timeToRampIn10msIntervals);
			}

		public static void StopRamp (this DeviceUShortInputCollection inputs, uint index)
			{
			inputs[index].StopRamp ();
			}

		public static void StopRamp (this DeviceUShortInputCollection inputs, string name)
			{
			inputs[name].StopRamp ();
			}

		public static void StopRamp (this BasicTriList device, uint index)
			{
			device.UShortInput[index].StopRamp ();
			}

		public static void StopRamp (this BasicTriList device, string name)
			{
			device.UShortInput[name].StopRamp ();
			}

		public static void StopRamp (this CrestronDeviceWithEvents device, uint index)
			{
			device.UShortInput[index].StopRamp ();
			}

		public static void StopRamp (this CrestronDeviceWithEvents device, string name)
			{
			device.UShortInput[name].StopRamp ();
			}

		public static void StopRamp (this SmartObject smartObject, uint index)
			{
			smartObject.UShortInput[index].StopRamp ();
			}

		public static void StopRamp (this SmartObject smartObject, string name)
			{
			smartObject.UShortInput[name].StopRamp ();
			}
		}
	}