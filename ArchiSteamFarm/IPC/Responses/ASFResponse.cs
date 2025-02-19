// ----------------------------------------------------------------------------------------------
//     _                _      _  ____   _                           _____
//    / \    _ __  ___ | |__  (_)/ ___| | |_  ___   __ _  _ __ ___  |  ___|__ _  _ __  _ __ ___
//   / _ \  | '__|/ __|| '_ \ | |\___ \ | __|/ _ \ / _` || '_ ` _ \ | |_  / _` || '__|| '_ ` _ \
//  / ___ \ | |  | (__ | | | || | ___) || |_|  __/| (_| || | | | | ||  _|| (_| || |   | | | | | |
// /_/   \_\|_|   \___||_| |_||_||____/  \__|\___| \__,_||_| |_| |_||_|   \__,_||_|   |_| |_| |_|
// ----------------------------------------------------------------------------------------------
// |
// Copyright 2015-2025 Łukasz "JustArchi" Domeradzki
// Contact: JustArchi@JustArchi.net
// |
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// |
// http://www.apache.org/licenses/LICENSE-2.0
// |
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ArchiSteamFarm.Storage;

namespace ArchiSteamFarm.IPC.Responses;

public sealed class ASFResponse {
	[Description("ASF's build variant")]
	[JsonInclude]
	[JsonRequired]
	[Required]
	public string BuildVariant { get; private init; }

	[Description("A value specifying whether this variant of ASF is capable of auto-update")]
	[JsonInclude]
	[JsonRequired]
	[Required]
	public bool CanUpdate { get; private init; }

	[Description("Currently loaded ASF's global config")]
	[JsonInclude]
	[JsonRequired]
	[Required]
	public GlobalConfig GlobalConfig { get; private init; }

	[Description("Current amount of managed memory being used by the process, in kilobytes")]
	[JsonInclude]
	[JsonRequired]
	[Required]
	public uint MemoryUsage { get; private init; }

	[Description("Start date of the process")]
	[JsonInclude]
	[JsonRequired]
	[Required]
	public DateTime ProcessStartTime { get; private init; }

	[Description("Boolean value specifying whether ASF has been started with a --service parameter")]
	[JsonInclude]
	[JsonRequired]
	[Required]
	public bool Service { get; private init; }

	[Description("ASF version of currently running binary")]
	[JsonInclude]
	[JsonRequired]
	[Required]
	public Version Version { get; private init; }

	internal ASFResponse(string buildVariant, bool canUpdate, GlobalConfig globalConfig, uint memoryUsage, DateTime processStartTime, Version version) {
		ArgumentException.ThrowIfNullOrEmpty(buildVariant);
		ArgumentNullException.ThrowIfNull(globalConfig);
		ArgumentOutOfRangeException.ThrowIfZero(memoryUsage);
		ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(processStartTime, DateTime.UnixEpoch);
		ArgumentNullException.ThrowIfNull(version);

		BuildVariant = buildVariant;
		CanUpdate = canUpdate;
		GlobalConfig = globalConfig;
		MemoryUsage = memoryUsage;
		ProcessStartTime = processStartTime;
		Version = version;

		Service = Program.Service;
	}
}
