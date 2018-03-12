﻿ /*****************************************
Table  Name:CommentShow
Create Time:2016/9/8 13:30:10
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>CommentShow
	/// This entiy code is generated by codesmith. this entity is CommentShow.
	/// </summary>
	[Serializable()]
	public class CommentShowEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 用户评论显示表
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
		/// 标题：收货N天后评论
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
		/// 评论时间
		/// <summary>
		private  DateTime _CommentTime;
	 	public DateTime CommentTime
		{
			get
			{
				return _CommentTime;
			}
			set
			{
				_CommentTime=value;
			}
		}

	    /// <summary>
		/// 星级
		/// <summary>
		private  int _Star;
	 	public int Star
		{
			get
			{
				return _Star;
			}
			set
			{
				_Star=value;
			}
		}

	    /// <summary>
		/// 颜色尺码等
		/// <summary>
		private  string _ColorSize;
	 	public string ColorSize
		{
			get
			{
				return _ColorSize;
			}
			set
			{
				_ColorSize=value;
			}
		}

	    /// <summary>
		/// 评论内容
		/// <summary>
		private  string _CommentContent;
	 	public string CommentContent
		{
			get
			{
				return _CommentContent;
			}
			set
			{
				_CommentContent=value;
			}
		}

	    /// <summary>
		/// 用户账号
		/// <summary>
		private  string _MemberCode;
	 	public string MemberCode
		{
			get
			{
				return _MemberCode;
			}
			set
			{
				_MemberCode=value;
			}
		}

	    /// <summary>
		/// 用户级别
		/// <summary>
		private  int _MemberLevel;
	 	public int MemberLevel
		{
			get
			{
				return _MemberLevel;
			}
			set
			{
				_MemberLevel=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  int _ClientType;
	 	public int ClientType
		{
			get
			{
				return _ClientType;
			}
			set
			{
				_ClientType=value;
			}
		}

		#endregion				
	}
}
