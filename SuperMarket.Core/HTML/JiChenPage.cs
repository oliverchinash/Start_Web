using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Core
{
    public class JiChenPage
    {
        #region 历史方式
        public static string SetMemberPage(int recordCount, int pageSize, int currentPage, string url)
        {
            int pageCount = 1;
            if (recordCount != 0)
            {
                if (recordCount % pageSize == 0)
                {
                    pageCount = recordCount / pageSize; // 获得总页数
                }
                else
                {
                    pageCount = recordCount / pageSize + 1; // 获得总页数
                }
            }

            string pagestr = "";
            if (recordCount > pageSize)
            {
                if (currentPage != 1)
                {
                    pagestr += "<a class='page-txt' href='" + url + "&page=1'>首页</a>";
                    pagestr += "<a class='page-txt' href='" + url + "&page=" + (currentPage - 1).ToString() + "'>上一页</a>";
                }
                else
                {
                    pagestr += "<span>首页</span>";
                    pagestr += "<span>上一页</span>";
                }

                bool p = false;
                bool n = false;
                for (int i = 0; i < pageCount; i++)
                {
                    if (i == currentPage - 1)
                    {
                        pagestr += "<a href='" + url + "&page=" + (i + 1).ToString() + "'><b>" + (i + 1) + "</b></a>";
                    }
                    else
                    {
                        if (i == 0 || i == pageCount - 1 || ((i > currentPage - 4) && (i < currentPage + 2)) || (currentPage < 4 && i < 6) || (currentPage > pageCount - 4 && i > pageCount - 7))
                        {
                            pagestr += "<a href='" + url + "&page=" + (i + 1).ToString() + "'>" + (i + 1) + "</a>";
                        }
                        else if (i < currentPage && !p)
                        {
                            p = true;
                            if (currentPage > 21)
                            {
                                pagestr += "<a href='" + url + "&page=" + (pageCount - 6).ToString() + "'>...</a>";
                            }
                            else
                            {
                                pagestr += "<a href='" + url + "&page=" + (currentPage - 3).ToString() + "'>...</a>";
                            }
                        }
                        else if (i > currentPage && !n)
                        {
                            n = true;
                            if (currentPage < 4)
                            {
                                pagestr += "<a href='" + url + "&page=" + (7).ToString() + "'>...</a>";
                            }
                            else
                            {
                                pagestr += "<a href='" + url + "&page=" + (currentPage + 3).ToString() + "'>...</a>";
                            }

                        }
                    }
                }
                if (currentPage != pageCount)
                {
                    pagestr += "<a class='page-txt' href='" + url + "&page=" + (currentPage + 1).ToString() + "'>下一页</a>";
                    pagestr += "<a class='page-txt' href='" + url + "&page=" + pageCount + "'>末页</a>";
                }
                else
                {
                    pagestr += "<span>下一页</span>";
                    pagestr += "<span>末页</span>";
                }
                pagestr = pagestr.Replace("?&", "?");
                pagestr = pagestr.Replace("&&", "&");

                pagestr = "<span>共" + recordCount + "条 第" + currentPage + "页/共" + pageCount + "页,每页" + pageSize + "条</span>" + pagestr;
            }

            return pagestr;
        }

        public static string SetProductListPage(int recordCount, int pageSize, int currentPage, string url)
        {

            int pageCount = 1;
            if (recordCount != 0)
            {
                if (recordCount % pageSize == 0)
                {
                    pageCount = recordCount / pageSize; // 获得总页数
                }
                else
                {
                    pageCount = recordCount / pageSize + 1; // 获得总页数
                }
            }

            string pagestr = "";
            if (recordCount > pageSize)
            {
                if (currentPage != 1)//如果不是第一页则需要给首页和上一页赋值
                {
                    pagestr += "<li  class='Page-num'><a  href='" + url + "&pageindex=1'>首页</a></li>";
                    pagestr += "<li  class='Page-num'><a   href='" + url + "&pageindex=" + (currentPage - 1).ToString() + "'>上一页</a></li>";
                }
                else
                {
                    pagestr += "<li  class='Page-num'><span>首页</span></li>";
                    pagestr += "<li  class='Page-num'><span>上一页</span></li>";
                }

                bool p = false;//previous
                bool n = false;//next
                for (int i = 0; i < pageCount; i++)
                {
                    if (i == currentPage - 1)//选择到当前页则是激活状态
                    {
                        pagestr += "<li  class='Page-num active'><a href='" + url + "&pageindex=" + (i + 1).ToString() + "'>" + (i + 1) + "</a></li>";
                    }

                    else
                    {
                        if (i == 0 || i == pageCount - 1 || ((i > currentPage - 4) && (i < currentPage + 2)) || (currentPage < 4 && i < 6) || (currentPage > pageCount - 4 && i > pageCount - 7))
                        {
                            pagestr += "<li  class='Page-num'><a href='" + url + "&pageindex=" + (i + 1).ToString() + "'>" + (i + 1) + "</a></li>";
                        }

                        else if (i < currentPage && !p)//一次成功
                        {
                            p = true;
                            if (currentPage > 21)
                            {
                                pagestr += "<li  class='Page-num'><a href='" + url + "&pageindex=" + (pageCount - 6).ToString() + "'>...</a></li>";
                            }
                            else
                            {
                                pagestr += "<li  class='Page-num'><a href='" + url + "&pageindex=" + (currentPage - 3).ToString() + "'>...</a></li>";
                            }
                        }

                        else if (i > currentPage && !n)//一次成功
                        {
                            n = true;
                            if (currentPage < 4)
                            {
                                pagestr += "<li  class='Page-num'><a href='" + url + "&pageindex=" + (7).ToString() + "'>...</a></li>";
                            }
                            else
                            {
                                pagestr += "<li  class='Page-num'><a href='" + url + "&pageindex=" + (currentPage + 3).ToString() + "'>...</a></li>";
                            }

                        }
                    }
                }

                if (currentPage != pageCount)
                {
                    pagestr += "<li  class='Page-num'><a  href='" + url + "&pageindex=" + (currentPage + 1).ToString() + "'>下一页</a></li>";
                    pagestr += "<li  class='Page-num'><a  href='" + url + "&pageindex=" + pageCount + "'>末页</a></li>";
                }
                else
                {
                    pagestr += "<li  class='Page-num'><span>下一页</span></li>";
                    pagestr += "<li  class='Page-num'><span>末页</span></li>";
                }

            }

            return pagestr;
        }

        public static string SetEvaluationListPage(int recordCount, int pageSize, int currentPage, string url)
        {
            {
                int pageCount = 1;
                if (recordCount != 0)
                {
                    if (recordCount % pageSize == 0)
                    {
                        pageCount = recordCount / pageSize; // 获得总页数
                    }
                    else
                    {
                        pageCount = recordCount / pageSize + 1; // 获得总页数
                    }
                }

                string pagestr = "";
                if (recordCount > pageSize)
                {
                    if (currentPage != 1)//如果不是第一页则需要给首页和上一页赋值
                    {
                        pagestr += "<li  class='firstorlast'><a  href='" + url + "&pageindex=1'>首页</a></li>";
                        pagestr += "<li  class='firstorlast'><a   href='" + url + "&pageindex=" + (currentPage - 1).ToString() + "'>上一页</a></li>";
                    }
                    else
                    {
                        pagestr += "<li  class='firstorlast'><span>首页</span></li>";
                        pagestr += "<li  class='firstorlast'><span>上一页</span></li>";
                    }

                    bool p = false;//previous
                    bool n = false;//next
                    for (int i = 0; i < pageCount; i++)
                    {
                        if (i == currentPage - 1)//选择到当前页则是激活状态 
                        {
                            pagestr += "<li  class='Page-num active'><a href='" + url + "&pageindex=" + (i + 1).ToString() + "'>" + (i + 1) + "</a></li>";
                        }

                        else
                        {
                            if (i == 0 || i == pageCount - 1 || ((i > currentPage - 4) && (i < currentPage + 2)) || (currentPage < 4 && i < 6) || (currentPage > pageCount - 4 && i > pageCount - 7))
                            {
                                pagestr += "<li  class='Page-num'><a href='" + url + "&pageindex=" + (i + 1).ToString() + "'>" + (i + 1) + "</a></li>";
                            }

                            else if (i < currentPage && !p)//一次成功
                            {
                                p = true;
                                if (currentPage > 21)
                                {
                                    pagestr += "<li  class='Page-num'><a href='" + url + "&pageindex=" + (pageCount - 6).ToString() + "'>...</a></li>";
                                }
                                else
                                {
                                    pagestr += "<li  class='Page-num'><a href='" + url + "&pageindex=" + (currentPage - 3).ToString() + "'>...</a></li>";
                                }
                            }

                            else if (i > currentPage && !n)//一次成功
                            {
                                n = true;
                                if (currentPage < 4)
                                {
                                    pagestr += "<li  class='Page-num'><a href='" + url + "&pageindex=" + (7).ToString() + "'>...</a></li>";
                                }
                                else
                                {
                                    pagestr += "<li  class='Page-num'><a href='" + url + "&pageindex=" + (currentPage + 3).ToString() + "'>...</a></li>";
                                }

                            }
                        }
                    }

                    if (currentPage != pageCount)
                    {
                        pagestr += "<li  class='firstorlast'><a  href='" + url + "&pageindex=" + (currentPage + 1).ToString() + "'>下一页</a></li>";
                        pagestr += "<li  class='firstorlast'><a  href='" + url + "&pageindex=" + pageCount + "'>末页</a></li>";
                    }
                    else
                    {
                        pagestr += "<li  class='firstorlast'><span>下一页</span></li>";
                        pagestr += "<li  class='firstorlast'><span>末页</span></li>";
                    }

                }

                return pagestr;
            }
        }
        public static string SetOrderListPage(int recordCount, int pageSize, int currentPage, string url)
        {
            {
                int pageCount = 1;
                if (recordCount != 0)
                {
                    if (recordCount % pageSize == 0)
                    {
                        pageCount = recordCount / pageSize; // 获得总页数
                    }
                    else
                    {
                        pageCount = recordCount / pageSize + 1; // 获得总页数
                    }
                }

                string pagestr = "";
                if (recordCount > pageSize)
                {
                    if (currentPage != 1)//如果不是第一页则需要给首页和上一页赋值
                    {
                        pagestr += "<a  class='firstorlast'  href='" + url + "&pageindex=1'>首页</a>";
                        pagestr += "<a  class='firstorlast'   href='" + url + "&pageindex=" + (currentPage - 1).ToString() + "'>上一页</a> ";
                    }
                    else
                    {
                        pagestr += "<a  class='firstorlast'><span>首页</span></a>";
                        pagestr += "<a  class='firstorlast'><span>上一页</span></a>";
                    }

                    bool p = false;//previous
                    bool n = false;//next
                    for (int i = 0; i < pageCount; i++)
                    {
                        if (i == currentPage - 1)//选择到当前页则是激活状态 
                        {
                            pagestr += "<a  class='Page-num active'  href='" + url + "&pageindex=" + (i + 1).ToString() + "'>" + (i + 1) + "</a> ";
                        }

                        else
                        {
                            if (i == 0 || i == pageCount - 1 || ((i > currentPage - 4) && (i < currentPage + 2)) || (currentPage < 4 && i < 6) || (currentPage > pageCount - 4 && i > pageCount - 7))
                            {
                                pagestr += "<a  class='Page-num'  href='" + url + "&pageindex=" + (i + 1).ToString() + "'>" + (i + 1) + "</a> ";
                            }

                            else if (i < currentPage && !p)//一次成功
                            {
                                p = true;
                                if (currentPage > 21)
                                {
                                    pagestr += "<a  class='Page-num' href='" + url + "&pageindex=" + (pageCount - 6).ToString() + "'>...</a> ";
                                }
                                else
                                {
                                    pagestr += "<a  class='Page-num' href='" + url + "&pageindex=" + (currentPage - 3).ToString() + "'>...</a> ";
                                }
                            }

                            else if (i > currentPage && !n)//一次成功
                            {
                                n = true;
                                if (currentPage < 4)
                                {
                                    pagestr += "<a  class='Page-num'  href='" + url + "&pageindex=" + (7).ToString() + "'>...</a> ";
                                }
                                else
                                {
                                    pagestr += "<a  class='Page-num'  href='" + url + "&pageindex=" + (currentPage + 3).ToString() + "'>...</a> ";
                                }

                            }
                        }
                    }

                    if (currentPage != pageCount)
                    {
                        pagestr += "<a  class='firstorlast'   href='" + url + "&pageindex=" + (currentPage + 1).ToString() + "'>下一页</a> ";
                        pagestr += "<a  class='firstorlast'   href='" + url + "&pageindex=" + pageCount + "'>末页</a> ";
                    }
                    else
                    {
                        pagestr += "<a  class='firstorlast'><span>下一页</span></a>";
                        pagestr += "<a  class='firstorlast'><span>末页</span></a>";
                    }

                }

                return pagestr;
            }
        }


        /// <summary>
        /// 设置订单
        /// </summary>
        /// <returns></returns>
        public static string SetCGOrderListPage(int recordCount, int pageSize, int currentPage, string url)
        {
            {
                int pageCount = 1;
                if (recordCount != 0)
                {
                    if (recordCount % pageSize == 0)
                    {
                        pageCount = recordCount / pageSize; // 获得总页数
                    }
                    else
                    {
                        pageCount = recordCount / pageSize + 1; // 获得总页数
                    }
                }

                string pagestr = "";
                if (recordCount > pageSize)
                {
                    if (currentPage != 1)//如果不是第一页则需要给首页和上一页赋值
                    {
                        pagestr += "<li  class='Order_page_item'><a  href='" + url + "&pageindex=1'>首页</a></li>";
                        pagestr += "<li  class='Order_page_item'><a   href='" + url + "&pageindex=" + (currentPage - 1).ToString() + "'>上一页</a></li>";
                    }
                    else
                    {
                        pagestr += "<li  class='Order_page_item'><span>首页</span></li>";
                        pagestr += "<li  class='Order_page_item'><span>上一页</span></li>";
                    }

                    bool p = false;//previous
                    bool n = false;//next
                    for (int i = 0; i < pageCount; i++)
                    {
                        if (i == currentPage - 1)//选择到当前页则是激活状态 
                        {
                            pagestr += "<li  class='Order_page_num' style='background:antiquewhite'><a href='" + url + "&pageindex=" + (i + 1).ToString() + "'>" + (i + 1) + "</a></li>";
                        }

                        else
                        {
                            if (i == 0 || i == pageCount - 1 || ((i > currentPage - 4) && (i < currentPage + 2)) || (currentPage < 4 && i < 6) || (currentPage > pageCount - 4 && i > pageCount - 7))
                            {
                                pagestr += "<li  class='Order_page_numOther Order_page_num'><a href='" + url + "&pageindex=" + (i + 1).ToString() + "'>" + (i + 1) + "</a></li>";
                            }

                            else if (i < currentPage && !p)//一次成功
                            {
                                p = true;
                                if (currentPage > 21)
                                {
                                    pagestr += "<li  class='Order_page_numOther Order_page_num'><a href='" + url + "&pageindex=" + (pageCount - 6).ToString() + "'>...</a></li>";
                                }
                                else
                                {
                                    pagestr += "<li  class='Order_page_numOther Order_page_num'><a href='" + url + "&pageindex=" + (currentPage - 3).ToString() + "'>...</a></li>";
                                }
                            }

                            else if (i > currentPage && !n)//一次成功
                            {
                                n = true;
                                if (currentPage < 4)
                                {
                                    pagestr += "<li  class='Order_page_numOther Order_page_num'><a href='" + url + "&pageindex=" + (7).ToString() + "'>...</a></li>";
                                }
                                else
                                {
                                    pagestr += "<li  class='Order_page_numOther Order_page_num'><a href='" + url + "&pageindex=" + (currentPage + 3).ToString() + "'>...</a></li>";
                                }

                            }
                        }
                    }

                    if (currentPage != pageCount)
                    {
                        pagestr += "<li  class='Order_page_item'><a  href='" + url + "&pageindex=" + (currentPage + 1).ToString() + "'>下一页</a></li>";
                        pagestr += "<li  class='Order_page_item'><a  href='" + url + "&pageindex=" + pageCount + "'>末页</a></li>";
                    }
                    else
                    {
                        pagestr += "<li  class='Order_page_item'><span>下一页</span></li>";
                        pagestr += "<li  class='Order_page_item'><span>末页</span></li>";
                    }

                }

                return pagestr;
            }
        }


        public static string SetReturnListPage(int recordCount, int pageSize, int currentPage, string url)
        {
            {
                int pageCount = 1;
                if (recordCount != 0)
                {
                    if (recordCount % pageSize == 0)
                    {
                        pageCount = recordCount / pageSize; // 获得总页数
                    }
                    else
                    {
                        pageCount = recordCount / pageSize + 1; // 获得总页数
                    }
                }

                string pagestr = "";
                if (recordCount > pageSize)
                {
                    if (currentPage != 1)//如果不是第一页则需要给首页和上一页赋值
                    {
                        pagestr += "<a  href='" + url + "&pageindex=1'><li  class='Order_page_item'>首页</li></a>";
                        pagestr += "<a   href='" + url + "&pageindex=" + (currentPage - 1).ToString() + "'><li  class='Order_page_item'>上一页</li></a>";
                    }
                    else
                    {
                        pagestr += "<li  class='Order_page_item'><span>首页</span></li>";
                        pagestr += "<li  class='Order_page_item'><span>上一页</span></li>";
                    }

                    bool p = false;//previous
                    bool n = false;//next
                    for (int i = 0; i < pageCount; i++)
                    {
                        if (i == currentPage - 1)//选择到当前页则是激活状态 
                        {
                            pagestr += "<a href='" + url + "&pageindex=" + (i + 1).ToString() + "'><li  class='Order_page_numCurrent'>" + (i + 1) + "</li></a>";
                        }

                        else
                        {
                            if (i == 0 || i == pageCount - 1 || ((i > currentPage - 4) && (i < currentPage + 2)) || (currentPage < 4 && i < 6) || (currentPage > pageCount - 4 && i > pageCount - 7))
                            {
                                pagestr += "<a href='" + url + "&pageindex=" + (i + 1).ToString() + "'><li  class='Order_page_num'>" + (i + 1) + "</li></a>";
                            }

                            else if (i < currentPage && !p)//一次成功
                            {
                                p = true;
                                if (currentPage > 21)
                                {
                                    pagestr += "<a href='" + url + "&pageindex=" + (pageCount - 6).ToString() + "'><li  class='Order_page_num'>...</li></a>";
                                }
                                else
                                {
                                    pagestr += "<a href='" + url + "&pageindex=" + (currentPage - 3).ToString() + "'><li  class='Order_page_num'>...</li></a>";
                                }
                            }

                            else if (i > currentPage && !n)//一次成功
                            {
                                n = true;
                                if (currentPage < 4)
                                {
                                    pagestr += "<a href='" + url + "&pageindex=" + (7).ToString() + "'><li  class='Order_page_num'>...</li></a>";
                                }
                                else
                                {
                                    pagestr += "<a href='" + url + "&pageindex=" + (currentPage + 3).ToString() + "'><li  class='Order_page_num'>...</li></a>";
                                }

                            }
                        }
                    }

                    if (currentPage != pageCount)
                    {
                        pagestr += "<a  href='" + url + "&pageindex=" + (currentPage + 1).ToString() + "'><li  class='Order_page_item'>下一页</li></a>";
                        pagestr += "<a  href='" + url + "&pageindex=" + pageCount + "'><li  class='Order_page_item'>末页</li></a>";
                    }
                    else
                    {
                        pagestr += "<li  class='Order_page_item'><span>下一页</span></li>";
                        pagestr += "<li  class='Order_page_item'><span>末页</span></li>";
                    }

                }

                return pagestr;
            }
        }

        public static string SetIssueContentPage(int recordCount, int pageSize, int currentPage, string url)
        {
            {
                int pageCount = 1;
                if (recordCount != 0)
                {
                    if (recordCount % pageSize == 0)
                    {
                        pageCount = recordCount / pageSize; // 获得总页数
                    }
                    else
                    {
                        pageCount = recordCount / pageSize + 1; // 获得总页数
                    }
                }

                string pagestr = "";
                if (recordCount > pageSize)
                {
                    if (currentPage != 1)//如果不是第一页则需要给首页和上一页赋值
                    {
                        pagestr += "<li  class='firstorlast'><a  href='" + url + "&pageindex=1'>首页</a></li>";
                        pagestr += "<li  class='firstorlast'><a   href='" + url + "&pageindex=" + (currentPage - 1).ToString() + "'>上一页</a></li>";
                    }
                    else
                    {
                        pagestr += "<li  class='firstorlast'><span>首页</span></li>";
                        pagestr += "<li  class='firstorlast'><span>上一页</span></li>";
                    }

                    bool p = false;//previous
                    bool n = false;//next
                    for (int i = 0; i < pageCount; i++)
                    {
                        if (i == currentPage - 1)//选择到当前页则是激活状态 
                        {
                            pagestr += "<li  class='Page-num active'><a href='" + url + "&pageindex=" + (i + 1).ToString() + "'>" + (i + 1) + "</a></li>";
                        }

                        else
                        {
                            if (i == 0 || i == pageCount - 1 || ((i > currentPage - 4) && (i < currentPage + 2)) || (currentPage < 4 && i < 6) || (currentPage > pageCount - 4 && i > pageCount - 7))
                            {
                                pagestr += "<li  class='Page-num'><a href='" + url + "&pageindex=" + (i + 1).ToString() + "'>" + (i + 1) + "</a></li>";
                            }

                            else if (i < currentPage && !p)//一次成功
                            {
                                p = true;
                                if (currentPage > 21)
                                {
                                    pagestr += "<li  class='Page-num'><a href='" + url + "&pageindex=" + (pageCount - 6).ToString() + "'>...</a></li>";
                                }
                                else
                                {
                                    pagestr += "<li  class='Page-num'><a href='" + url + "&pageindex=" + (currentPage - 3).ToString() + "'>...</a></li>";
                                }
                            }

                            else if (i > currentPage && !n)//一次成功
                            {
                                n = true;
                                if (currentPage < 4)
                                {
                                    pagestr += "<li  class='Page-num'><a href='" + url + "&pageindex=" + (7).ToString() + "'>...</a></li>";
                                }
                                else
                                {
                                    pagestr += "<li  class='Page-num'><a href='" + url + "&pageindex=" + (currentPage + 3).ToString() + "'>...</a></li>";
                                }

                            }
                        }
                    }

                    if (currentPage != pageCount)
                    {
                        pagestr += "<li  class='firstorlast'><a  href='" + url + "&pageindex=" + (currentPage + 1).ToString() + "'>下一页</a></li>";
                        pagestr += "<li  class='firstorlast'><a  href='" + url + "&pageindex=" + pageCount + "'>末页</a></li>";
                    }
                    else
                    {
                        pagestr += "<li  class='firstorlast'><span>下一页</span></li>";
                        pagestr += "<li  class='firstorlast'><span>末页</span></li>";
                    }

                }

                return pagestr;
            }
        }
        #endregion



        public static string GetHTMLListDivPage(int recordCount, int pageSize, int currentPage, string url)
        {

            int pageCount = 1;
            if (recordCount != 0)
            {
                if (recordCount % pageSize == 0)
                {
                    pageCount = recordCount / pageSize; // 获得总页数
                }
                else
                {
                    pageCount = recordCount / pageSize + 1; // 获得总页数
                }
            }

            string pagestr = "";
            if (recordCount > pageSize)
            {
                if (currentPage != 1)//如果不是第一页则需要给首页和上一页赋值
                {
                   pagestr += "<div class='Page_up'><a   href='" + url + "&pi=" + (currentPage - 1).ToString() + "'>上一页</a></div>";
                }
                else
                {
                      pagestr +="<div class='Page_up'> 上一页 </div>";
                 }

                bool p = false;//previous
                bool n = false;//next
                for (int i = 0; i < pageCount; i++)
                {
                    if (i == currentPage - 1)//选择到当前页则是激活状态
                    {
                        pagestr += "<div class='Page_num_active'> " + (i + 1) + " </div>";
                     }

                    else
                    {
                        if (i == 0 || i == pageCount - 1 || ((i > currentPage - 4) && (i < currentPage + 2)) || (currentPage < 4 && i < 6) || (currentPage > pageCount - 4 && i > pageCount - 7))
                        {
                            pagestr += "<div class='Page_num'><a href='" + url + "&pi=" + (i + 1).ToString() + "'>" + (i + 1) + "</a></div>";
                         }

                        else if (i < currentPage && !p)//一次成功
                        {
                            p = true;
                            if (currentPage > 21)
                            { 
                                pagestr += "<div class='Page_num'><a href='" + url + "&pi=" + (pageCount - 6).ToString() + "'>...</a></div>";
                            }
                            else
                            {
                                pagestr += "<div  class='Page-num'><a href='" + url + "&pi=" + (currentPage - 3).ToString() + "'>...</a></div>";
                            }
                        }

                        else if (i > currentPage && !n)//一次成功
                        {
                            n = true;
                            if (currentPage < 4)
                            {
                                pagestr += "<div  class='Page-num'><a href='" + url + "&pi=" + (7).ToString() + "'>...</a></div>";
                            }
                            else
                            {
                                pagestr += "<div  class='Page-num'><a href='" + url + "&pi=" + (currentPage + 3).ToString() + "'>...</a></div>";
                            } 
                        }
                    }
                } 
                if (currentPage != pageCount)
                { 
                    pagestr += "<div class='Page_down'><a  href='" + url + "&pi=" + (currentPage + 1).ToString() + "'>下一页</a></div>";
                }
                else
                {
                    pagestr += "<div class='Page_down'>下一页</div>"; 
                } 
            }

            return pagestr;
        }


        public static string GetDiscusssListDivPage(int recordCount, int pageSize, int currentPage, string url)
        {

            int pageCount = 1;
            if (recordCount != 0)
            {
                if (recordCount % pageSize == 0)
                {
                    pageCount = recordCount / pageSize; // 获得总页数
                }
                else
                {
                    pageCount = recordCount / pageSize + 1; // 获得总页数
                }
            }

            string pagestr = "";
            if (recordCount > pageSize)
            {
                pagestr += "< div class='Page'>";
                //if (currentPage != 1)//如果不是第一页则需要给首页和上一页赋值
                //{
                //    pagestr += "<div class='Page_up'><a   href='" + url + "&pi=" + (currentPage - 1).ToString() + "'>上一页</a></div>";
                //}
                //else
                //{
                //    pagestr += "<div class='Page_up'> 上一页 </div>";
                //}

                bool p = false;//previous
                bool n = false;//next
                for (int i = 0; i < pageCount; i++)
                {
                    if (i == currentPage - 1)//选择到当前页则是激活状态
                    {
                        pagestr += "<div class='Page_num_active'> " + (i + 1) + " </div>";
                    } 
                    else
                    {
                        if (i == 0 || i == pageCount - 1 || ((i > currentPage - 4) && (i < currentPage + 2)) || (currentPage < 4 && i < 6) || (currentPage > pageCount - 4 && i > pageCount - 7))
                        {
                            pagestr += "<div class='Page_num pagediscuss' url='" + url + "&pi=" + (i + 1).ToString() + "' >" + (i + 1) + "</div>";
                        }

                        else if (i < currentPage && !p)//一次成功
                        {
                            p = true;
                            if (currentPage > 21)
                            {
                                pagestr += "<div class='Page_num pagediscuss' url='" + url + "&pi=" + (pageCount - 6).ToString() + "' >...</div>";
                            }
                            else
                            {
                                pagestr += "<div  class='Page-num pagediscuss' url='" + url + "&pi=" + (currentPage - 3).ToString() + "'>...</div>";
                            }
                        } 
                        else if (i > currentPage && !n)//一次成功
                        {
                            n = true;
                            if (currentPage < 4)
                            {
                                pagestr += "<div  class='Page-num pagediscuss' url='" + url + "&pi=" + (7).ToString() + "'>...</div>";
                            }
                            else
                            {
                                pagestr += "<div  class='Page-num pagediscuss' url='" + url + "&pi=" + (currentPage + 3).ToString() + "'>...</div>";
                            }
                        }
                    }
                }
                pagestr += "</div>";
                //if (currentPage != pageCount)
                //{
                //    pagestr += "<div class='Page_down'><a  href='" + url + "&pi=" + (currentPage + 1).ToString() + "'>下一页</a></div>";
                //}
                //else
                //{
                //    pagestr += "<div class='Page_down'>下一页</div>";
                //}
            }

            return pagestr;
        }

        public static string GetSysListPage(int recordCount, int pageSize, int currentPage, string url)
        {

            int pageCount = 1;
            if (recordCount != 0)
            {
                if (recordCount % pageSize == 0)
                {
                    pageCount = recordCount / pageSize; // 获得总页数
                }
                else
                {
                    pageCount = recordCount / pageSize + 1; // 获得总页数
                }
            }

            string pagestr = "";
            if (recordCount > pageSize)
            {
                if (currentPage != 1)//如果不是第一页则需要给首页和上一页赋值
                { 
                    pagestr += "<li><a href='" + url + "&pageindex=" + (currentPage - 1).ToString() + "'><</a></li>";
                }
                else
                { 
                    pagestr += "<li class='list-page-disabled'><a><</a></li>";
                }

                bool p = false;//previous
                bool n = false;//next
                for (int i = 0; i < pageCount; i++)
                {
                    if (i == currentPage - 1)//选择到当前页则是激活状态
                    {
                        pagestr += "<li  class='list-page-active'><a href='" + url + "&pageindex=" + (i + 1).ToString() + "'>" + (i + 1) + "</a></li>";
                    }

                    else
                    {
                        if (i == 0 || i == pageCount - 1 || ((i > currentPage - 4) && (i < currentPage + 2)) || (currentPage < 4 && i < 6) || (currentPage > pageCount - 4 && i > pageCount - 7))
                        {
                            pagestr += "<li  ><a href='" + url + "&pageindex=" + (i + 1).ToString() + "'>" + (i + 1) + "</a></li>";
                        }

                        else if (i < currentPage && !p)//一次成功
                        {
                            p = true;
                            if (currentPage > 21)
                            {
                                pagestr += "<li ><a href='" + url + "&pageindex=" + (pageCount - 6).ToString() + "'>...</a></li>";
                            }
                            else
                            {
                                pagestr += "<li ><a href='" + url + "&pageindex=" + (currentPage - 3).ToString() + "'>...</a></li>";
                            }
                        }

                        else if (i > currentPage && !n)//一次成功
                        {
                            n = true;
                            if (currentPage < 4)
                            {
                                pagestr += "<li ><a href='" + url + "&pageindex=" + (7).ToString() + "'>...</a></li>";
                            }
                            else
                            {
                                pagestr += "<li ><a href='" + url + "&pageindex=" + (currentPage + 3).ToString() + "'>...</a></li>";
                            }

                        }
                    }
                }

                if (currentPage != pageCount)
                {
                    pagestr += "<li ><a  href='" + url + "&pageindex=" + (currentPage + 1).ToString() + "'>></a></li>";
                 }
                else
                {
                    pagestr += "<li   class='list-page-disabled'><a>></a></li>"; 
                }

            }

            return pagestr;
        }

      


    }
}
