// Copyright 2001-2016 Crytek GmbH / Crytek Group. All rights reserved.

using CryEngine.Attributes;
using System;
using System.Reflection;

namespace CryEngine
{
	/// <summary>
	/// Allows creating an entity class containing a default entity component that is spawned with every instance. 
    /// Only used for types inheriting from 'EntityComponent'.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public sealed class EntityClassAttribute : Attribute
	{
		#region Properties
		/// <summary>
		/// Display name inside CRYENGINE's EntitySystem as well as Sandbox.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Entity category inside Sandbox.
		/// </summary>
		public string EditorPath { get; set; }
		/// <summary>
		/// Geometry path for 3D helper.
		/// </summary>
		public string Helper { get; set; }
		/// <summary>
		/// Bitmap path for 2D helper.
		/// </summary>
		public string Icon { get; set; }
		/// <summary>
		/// If set, the marked entity class will not be displayed inside Sandbox.
		/// </summary>
		public bool Hide { get; set; }
		#endregion

		#region Constructors
		[Obsolete("Please use EntityClassAttribute(string name, string category, string helper, IconHelper.IconType icon, bool hide)")]
		public EntityClassAttribute(string name = "", string category = "Game", string helper = null, string icon = "prompt.bmp", bool hide = false)
		{
			Name = name;
			EditorPath = category;
			Helper = helper;
			Icon = icon;
			Hide = hide;
		}

		public EntityClassAttribute(string name = "", string category = "Game", string helper = null, IconType icon = IconType.None, bool hide = false)
		{
			Name = name;
			EditorPath = category;
			Helper = helper;
			Hide = hide;
			//get icon path attribute bounded to the icon path enum (if available)
			var enumType = icon.GetType();
			var enumMemberInfo = enumType.GetMember(icon.ToString());
			IconPathAttribute iconPathAttribute = (IconPathAttribute)enumMemberInfo[0].GetCustomAttributes(typeof(IconPathAttribute), true)[0];
			Icon = iconPathAttribute == null ? "": iconPathAttribute.Path;
		}
		#endregion
	}
    
	/// <summary>
	/// Attribute indicating that an entity represents a player
	/// This is used to automatically spawn entities when clients connect
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class PlayerEntityAttribute : Attribute
	{
	}
}