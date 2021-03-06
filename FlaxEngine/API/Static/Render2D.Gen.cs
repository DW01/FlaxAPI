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
	/// The interface to render fast two dimensional graphics.
	/// </summary>
	public static partial class Render2D
	{
		/// <summary>
		/// Pushes 2D transformation matrix on the stack.
		/// </summary>
		/// <param name="transform">The transformation.</param>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public static void PushTransform(Matrix3x3 transform) 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_PushTransform(ref transform);
#endif
		}

		/// <summary>
		/// Pops transformation matrix from the stack.
		/// </summary>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public static void PopTransform() 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_PopTransform();
#endif
		}

		/// <summary>
		/// Push clipping rectangle mask
		/// </summary>
		/// <param name="clipRect">Axis aligned clipping mask rectangle</param>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public static void PushClip(Rectangle clipRect) 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_PushClip(ref clipRect);
#endif
		}

		/// <summary>
		/// Pop clippng rectangle mask
		/// </summary>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public static void PopClip() 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_PopClip();
#endif
		}

		/// <summary>
		/// Draw text
		/// </summary>
		/// <param name="font">Font to use</param>
		/// <param name="text">Text to render</param>
		/// <param name="layoutRect">The size and position of the area in which the text is drawn</param>
		/// <param name="color">Text color</param>
		/// <param name="horizontalAlignment">Horizontal alignment of the text in a layout rectangle</param>
		/// <param name="verticalAlignment">Vetical alignment of the text in a layout rectangle</param>
		/// <param name="textWrapping">Describes how wrap text inside a layout rectangle</param>
		/// <param name="baseLinesGapScale">Scale for distance one baseline from another. Default is 1.</param>
		/// <param name="scale">Text drawing scale. Default is 1.</param>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public static void DrawText(Font font, string text, Rectangle layoutRect, Color color, TextAlignment horizontalAlignment = TextAlignment.Near, TextAlignment verticalAlignment = TextAlignment.Near, TextWrapping textWrapping = TextWrapping.NoWrap, float baseLinesGapScale = 1.0f, float scale = 1.0f) 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_DrawText(Object.GetUnmanagedPtr(font), text, ref layoutRect, ref color, horizontalAlignment, verticalAlignment, textWrapping, baseLinesGapScale, scale);
#endif
		}

		/// <summary>
		/// Fill rectangle area
		/// </summary>
		/// <param name="rect">Rectangle to fill</param>
		/// <param name="color">Color to use</param>
		/// <param name="withAlpha">True if use alpha blending, otherwise it will be disabled</param>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public static void FillRectangle(Rectangle rect, Color color, bool withAlpha = false) 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_FillRectangle1(ref rect, ref color, withAlpha);
#endif
		}

		/// <summary>
		/// Fill rectangle area
		/// </summary>
		/// <param name="rect">Rectangle to fill</param>
		/// <param name="color0">Color to use for upper left vertex</param>
		/// <param name="color1">Color to use for upper right vertex</param>
		/// <param name="color2">Color to use for bottom right vertex</param>
		/// <param name="color3">Color to use for bottom left vertex</param>
		/// <param name="withAlpha">True if use alpha blending, otherwise it will be disabled</param>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public static void FillRectangle(Rectangle rect, Color color0, Color color1, Color color2, Color color3, bool withAlpha = false) 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_FillRectangle2(ref rect, ref color0, ref color1, ref color2, ref color3, withAlpha);
#endif
		}

		/// <summary>
		/// Draw rectangle borders
		/// </summary>
		/// <param name="rect">Rectangle to draw</param>
		/// <param name="color">Color to use</param>
		/// <param name="withAlpha">True if use alpha blending, otherwise it will be disabled</param>
		/// <param name="thickness">Lines thickness (in pixels)</param>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public static void DrawRectangle(Rectangle rect, Color color, bool withAlpha = false, float thickness = 1.0f) 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_DrawRectangle(ref rect, ref color, withAlpha, thickness);
#endif
		}

		/// <summary>
		/// Draw texture
		/// </summary>
		/// <param name="rt">Render target to draw</param>
		/// <param name="rect">Rectangle to draw</param>
		/// <param name="color">Color to use</param>
		/// <param name="withAlpha">True if use alpha blending, otherwise it will be disabled.</param>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public static void DrawRenderTarget(Rendering.RenderTarget rt, Rectangle rect, Color color, bool withAlpha = false) 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_DrawRenderTarget(Object.GetUnmanagedPtr(rt), ref rect, ref color, withAlpha);
#endif
		}

		/// <summary>
		/// Draw texture
		/// </summary>
		/// <param name="t">Texture to draw</param>
		/// <param name="rect">Rectangle to draw</param>
		/// <param name="color">Color to use</param>
		/// <param name="withAlpha">True if use alpha blending, otherwise it will be disabled.</param>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public static void DrawTexture(Texture t, Rectangle rect, Color color, bool withAlpha = false) 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_DrawTexture1(Object.GetUnmanagedPtr(t), ref rect, ref color, withAlpha);
#endif
		}

		/// <summary>
		/// Draw texture
		/// </summary>
		/// <param name="t">Texture to draw</param>
		/// <param name="rect">Rectangle to draw</param>
		/// <param name="color">Color to use</param>
		/// <param name="withAlpha">True if use alpha blending, otherwise it will be disabled.</param>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public static void DrawTexture(SpriteAtlas t, Rectangle rect, Color color, bool withAlpha = false) 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_DrawTexture2(Object.GetUnmanagedPtr(t), ref rect, ref color, withAlpha);
#endif
		}

		/// <summary>
		/// Draw line
		/// </summary>
		/// <param name="p1">Start point</param>
		/// <param name="p2">End point</param>
		/// <param name="color">Color to use</param>
		/// <param name="thickness">Lines thickness (in pixels)</param>
		/// <param name="withAlpha">True if use alpha blending, otherwise it will be disabled.</param>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public static void DrawLine(Vector2 p1, Vector2 p2, Color color, float thickness = 1.0f, bool withAlpha = false) 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_DrawLine(ref p1, ref p2, ref color, thickness, withAlpha);
#endif
		}

		/// <summary>
		/// Draw Bezier curve
		/// </summary>
		/// <param name="p1">Start point</param>
		/// <param name="p2">First control point</param>
		/// <param name="p3">Second control point</param>
		/// <param name="p4">End point</param>
		/// <param name="color">Color to use</param>
		/// <param name="thickness">Lines thickness (in pixels)</param>
		/// <param name="withAlpha">True if use alpha blending, otherwise it will be disabled.</param>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public static void DrawBezier(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, Color color, float thickness = 1.0f, bool withAlpha = false) 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_DrawBezier(ref p1, ref p2, ref p3, ref p4, ref color, thickness, withAlpha);
#endif
		}

		/// <summary>
		/// Draws the postFx material in the 2D.
		/// </summary>
		/// <param name="material">Material to render. Must be a PostFx material type.</param>
		/// <param name="rect">The target rectangle to draw.</param>
#if UNIT_TEST_COMPILANT
		[Obsolete("Unit tests, don't support methods calls.")]
#endif
		[UnmanagedCall]
		public static void DrawMaterial(MaterialBase material, Rectangle rect) 
		{
#if UNIT_TEST_COMPILANT
			throw new NotImplementedException("Unit tests, don't support methods calls. Only properties can be get or set.");
#else
			Internal_DrawMaterial(Object.GetUnmanagedPtr(material), ref rect);
#endif
		}

#region Internal Calls
#if !UNIT_TEST_COMPILANT
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_PushTransform(ref Matrix3x3 transform);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_PopTransform();
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_PushClip(ref Rectangle clipRect);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_PopClip();
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_DrawText(IntPtr font, string text, ref Rectangle layoutRect, ref Color color, TextAlignment horizontalAlignment, TextAlignment verticalAlignment, TextWrapping textWrapping, float baseLinesGapScale, float scale);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_FillRectangle1(ref Rectangle rect, ref Color color, bool withAlpha);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_FillRectangle2(ref Rectangle rect, ref Color color0, ref Color color1, ref Color color2, ref Color color3, bool withAlpha);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_DrawRectangle(ref Rectangle rect, ref Color color, bool withAlpha, float thickness);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_DrawRenderTarget(IntPtr rt, ref Rectangle rect, ref Color color, bool withAlpha);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_DrawTexture1(IntPtr t, ref Rectangle rect, ref Color color, bool withAlpha);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_DrawTexture2(IntPtr t, ref Rectangle rect, ref Color color, bool withAlpha);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_DrawLine(ref Vector2 p1, ref Vector2 p2, ref Color color, float thickness, bool withAlpha);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_DrawBezier(ref Vector2 p1, ref Vector2 p2, ref Vector2 p3, ref Vector2 p4, ref Color color, float thickness, bool withAlpha);
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_DrawMaterial(IntPtr material, ref Rectangle rect);
#endif
#endregion
	}
}

