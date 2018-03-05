////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2012-2018 Flax Engine. All rights reserved.
////////////////////////////////////////////////////////////////////////////////////
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Runtime.CompilerServices;

namespace FlaxEngine
{
	/// <summary>
	/// Represents a source for emitting audio. Audio can be played spatially (gun shot), or normally (music). Each audio source must have an AudioClip to play - back, and it can also have a position in the case of spatial(3D) audio.
	/// </summary>
	/// <remarks>
	/// Whether or not an audio source is spatial is controlled by the assigned AudioClip.The volume and the pitch of a spatial audio source is controlled by its position and the AudioListener's position/direction/velocity.
	/// </remarks>
	[Serializable]
	public sealed partial class AudioSource : Actor
	{
		/// <summary>
		/// Creates new <see cref="AudioSource"/> object.
		/// </summary>
		private AudioSource() : base()
		{
		}

		/// <summary>
		/// Creates new instance of <see cref="AudioSource"/> object.
		/// </summary>
		/// <returns>Created object.</returns>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public static AudioSource New() 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			return Internal_Create(typeof(AudioSource)) as AudioSource;
#endif
		}

		/// <summary>
		/// Gets or sets the audio clip asset used as a source of the sound.
		/// </summary>
		[UnmanagedCall]
		[EditorOrder(10), EditorDisplay("Audio Source"), Tooltip("The audio clip asset used as a source of the sound.")]
		public AudioClip Clip
		{
#if UNIT_TEST_COMPILANT
			get; set;
#else
			get { return Internal_GetClip(unmanagedPtr); }
			set { Internal_SetClip(unmanagedPtr, Object.GetUnmanagedPtr(value)); }
#endif
		}

		/// <summary>
		/// Gets or sets the volume of the audio played from this source, in [0, 1] range.
		/// </summary>
		[UnmanagedCall]
		[EditorOrder(20), Limit(0, 1, 0.01f), EditorDisplay("Audio Source"), Tooltip("The volume of the audio played from this source, in [0, 1] range.")]
		public float Volume
		{
#if UNIT_TEST_COMPILANT
			get; set;
#else
			get { return Internal_GetVolume(unmanagedPtr); }
			set { Internal_SetVolume(unmanagedPtr, value); }
#endif
		}

		/// <summary>
		/// Gets or sets the pitch of the played audio. The default is 1.
		/// </summary>
		[UnmanagedCall]
		[EditorOrder(30), Limit(0.5f, 2.0f, 0.01f), EditorDisplay("Audio Source"), Tooltip("The pitch of the played audio. The default is 1.")]
		public float Pitch
		{
#if UNIT_TEST_COMPILANT
			get; set;
#else
			get { return Internal_GetPitch(unmanagedPtr); }
			set { Internal_SetPitch(unmanagedPtr, value); }
#endif
		}

		/// <summary>
		/// Determines whether the audio clip should loop when it finishes playing.
		/// </summary>
		[UnmanagedCall]
		[EditorOrder(40), EditorDisplay("Audio Source"), Tooltip("Determines whether the audio clip should loop when it finishes playing.")]
		public bool IsLooping
		{
#if UNIT_TEST_COMPILANT
			get; set;
#else
			get { return Internal_GetIsLooping(unmanagedPtr); }
			set { Internal_SetIsLooping(unmanagedPtr, value); }
#endif
		}

		/// <summary>
		/// Determines whether the audio clip should auto play on level start.
		/// </summary>
		[UnmanagedCall]
		[EditorOrder(50), EditorDisplay("Audio Source", "Play On Start"), Tooltip("Determines whether the audio clip should auto play on level start.")]
		public bool PlayOnStart
		{
#if UNIT_TEST_COMPILANT
			get; set;
#else
			get { return Internal_GetPlayOnStart(unmanagedPtr); }
			set { Internal_SetPlayOnStart(unmanagedPtr, value); }
#endif
		}

		/// <summary>
		/// Gets or sets the minimum distance at which audio attenuation starts. When the listener is closer to the source than this value, audio is heard at full volume. Once farther away the audio starts attenuating.
		/// </summary>
		[UnmanagedCall]
		[EditorOrder(60), Limit(0, float.MaxValue, 0.1f), EditorDisplay("Audio Source"), Tooltip("The minimum distance at which audio attenuation starts. When the listener is closer to the source than this value, audio is heard at full volume. Once farther away the audio starts attenuating.")]
		public float MinDistance
		{
#if UNIT_TEST_COMPILANT
			get; set;
#else
			get { return Internal_GetMinDistance(unmanagedPtr); }
			set { Internal_SetMinDistance(unmanagedPtr, value); }
#endif
		}

		/// <summary>
		/// Gets or sets the attenuation that controls how quickly does audio volume drop off as the listener moves further from the source.
		/// </summary>
		[UnmanagedCall]
		[EditorOrder(70), Limit(0, float.MaxValue, 0.1f), EditorDisplay("Audio Source"), Tooltip("The attenuation that controls how quickly does audio volume drop off as the listener moves further from the source.")]
		public float Attenuation
		{
#if UNIT_TEST_COMPILANT
			get; set;
#else
			get { return Internal_GetAttenuation(unmanagedPtr); }
			set { Internal_SetAttenuation(unmanagedPtr, value); }
#endif
		}

		/// <summary>
		/// Starts playing the currently assigned audio clip.
		/// </summary>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public void Play() 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_Play(unmanagedPtr);
#endif
		}

		/// <summary>
		/// Pauses the audio playback.
		/// </summary>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public void Pause() 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_Pause(unmanagedPtr);
#endif
		}

		/// <summary>
		/// Stops audio playback, rewinding it to the start.
		/// </summary>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public void Stop() 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_Stop(unmanagedPtr);
#endif
		}

		/// <summary>
		/// Gets the the current state of the audio playback (playing/paused/stopped).
		/// </summary>
		[UnmanagedCall]
		public States State
		{
#if UNIT_TEST_COMPILANT
			get; set;
#else
			get { return Internal_GetState(unmanagedPtr); }
#endif
		}

		/// <summary>
		/// Gets or sets the current time of playback. If playback hasn't yet started, it specifies the time at which playback will start at. The time is in seconds, in range [0, ClipLength].
		/// </summary>
		[UnmanagedCall]
		[HideInEditor]
		public float Time
		{
#if UNIT_TEST_COMPILANT
			get; set;
#else
			get { return Internal_GetTime(unmanagedPtr); }
			set { Internal_SetTime(unmanagedPtr, value); }
#endif
		}

#region Internal Calls
#if !UNIT_TEST_COMPILANT
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AudioClip Internal_GetClip(IntPtr obj);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_SetClip(IntPtr obj, IntPtr val);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float Internal_GetVolume(IntPtr obj);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_SetVolume(IntPtr obj, float val);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float Internal_GetPitch(IntPtr obj);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_SetPitch(IntPtr obj, float val);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Internal_GetIsLooping(IntPtr obj);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_SetIsLooping(IntPtr obj, bool val);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Internal_GetPlayOnStart(IntPtr obj);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_SetPlayOnStart(IntPtr obj, bool val);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float Internal_GetMinDistance(IntPtr obj);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_SetMinDistance(IntPtr obj, float val);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float Internal_GetAttenuation(IntPtr obj);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_SetAttenuation(IntPtr obj, float val);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Play(IntPtr obj);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Pause(IntPtr obj);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Stop(IntPtr obj);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern States Internal_GetState(IntPtr obj);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float Internal_GetTime(IntPtr obj);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_SetTime(IntPtr obj, float val);
#endif
#endregion
	}
}
