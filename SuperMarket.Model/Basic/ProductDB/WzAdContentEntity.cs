﻿ /*****************************************
Table  Name:WzAdContent
Create Time:2016/8/16 13:54:29
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>WzAdContent
	/// This entiy code is generated by codesmith. this entity is WzAdContent.
	/// </summary>
	[Serializable()]
	public class WzAdContentEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 
		/// <summary>
		private  int _Id;
	 	public int Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _Title;
	 	public string Title
		{
			get
			{
				return _Title;
			}
			set
			{
				_Title=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _HTMLContent;
	 	public string HTMLContent
		{
			get
			{
				return _HTMLContent;
			}
			set
			{
				_HTMLContent=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  DateTime _CreateTime;
	 	public DateTime CreateTime
		{
			get
			{
				return _CreateTime;
			}
			set
			{
				_CreateTime=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  int _UpdateManId;
	 	public int UpdateManId
		{
			get
			{
				return _UpdateManId;
			}
			set
			{
				_UpdateManId=value;
			}
		}

		#endregion				
	}
}