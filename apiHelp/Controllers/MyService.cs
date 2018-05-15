using apiHelp;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;

namespace apiHelp.Controllers
{
    [RoutePrefix("api/CBOF_BASE_DATA_Api")]
	public partial class CBOF_BASE_DATA_ApiController :ApiController
    {	     
	
	    Entities db=new Entities();
		
	

	    /// <summary>
        /// 加载单表数据
        /// </summary>
		/// <returns>message消息</returns> 
		[Route("Load")]
        [HttpGet]
		//序列化类型为“System.Data.Entity.DynamicProxies.Photos....这个会的对象时检测到循环引用。
		// db.Configuration.LazyLoadingEnabled = false;
		//  db.Configuration.ProxyCreationEnabled = false;
		//因为这个表和另一个表是有一对多关系的,当序列化表1的时候,会找到和另一个表2关联的字段,就会到另一个表2中序列化,
		//然后另一个表2中也有一个字段和表1相关联.这样.序列化就会发生这种错误
		public   Message<CBOF_BASE_DATA> Load()
        { 		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.CBOF_BASE_DATA.AsNoTracking().ToList(); 
				Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();
				//ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        /// 单表有效值查询
        /// </summary>
        /// <returns>message消息</returns>
		[Route("LoadValid")]
        [HttpGet]
		public  Message<CBOF_BASE_DATA> LoadValid()
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data= db.CBOF_BASE_DATA.AsNoTracking().Where(o=>o.DR=="0").ToList(); 
				Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();
				//ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
			    Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
                
            }
		                    
        }

		 /// <summary>
        ///  分页查询
        /// </summary>
        /// <param name="limit">一页多少条</param>
        /// <param name="offset">第几页</param>
        /// <returns>message消息</returns>

		[Route("LoadPage")]
        [HttpGet]
		//返回类型是Message 报错“ObjectContent`1”类型未能序列化内容类型“application/xml; charset=utf-8”的响应正文。 
		public Message<CBOF_BASE_DATA> LoadPage(int limit,int offset)
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.CBOF_BASE_DATA.AsNoTracking().OrderBy(o=>o.TS).Skip(offset).Take(limit).ToList();
				var count=db.CBOF_BASE_DATA.Count();
				Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();
				ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
			    Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
               
            }
		                    
        }


		 /// <summary>
        ///  根据id值查询单条数据
        /// </summary>
        /// <param name="id">主键id值</param>
        /// <returns>message消息</returns>		
		[Route("Find")]
        [HttpGet]
		public  Message<CBOF_BASE_DATA> Find(string id)
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
			    List<CBOF_BASE_DATA> list=new List<CBOF_BASE_DATA>();
			    var data=db.CBOF_BASE_DATA.Find(id);
				Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();				
				ms.row=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        ///  根据id主键查询并删除
        /// </summary>
        /// <param name="id">主键id值</param>
        /// <returns>message消息</returns>		
		[Route("FindDelete")]
        [HttpGet]
		public  Message<CBOF_BASE_DATA> FindDelete(string id)
        { 		     
		    try
            {	
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;				
				Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();
			    var data=db.CBOF_BASE_DATA.Find(id);
				if(data!=null)
				{
				  db.CBOF_BASE_DATA.Remove(data);
				  db.SaveChangesAsync();
				}							
				 ms.success="true";
				 ms.message="操作成功";
                 return ms;
               
            }
            catch (Exception ex)
            {
                Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        ///  单表时间查询 大于开始时间 小于结束时间
        /// </summary>
        /// <param name="begin">开始时间</param>
		 /// <param name="begin">结束时间</param>
        /// <returns>message消息</returns>	
		[Route("LoadByTs")]
        [HttpGet]
		public  Message<CBOF_BASE_DATA> LoadByTs(string begin,string end)
        { 
		     
		    try
            {   
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.CBOF_BASE_DATA.AsNoTracking().Where(o=>o.TS.CompareTo(begin)>=0&&o.TS.CompareTo(end)<=0).ToList(); 
				Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();				
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }


		 /// <summary>
        ///  单表条件查询 sql
        /// </summary>
        /// <param name="sql">查询条件</param>
        /// <returns>message消息</returns>	
		[Route("Query")]
        [HttpGet]
		public  Message<CBOF_BASE_DATA> Query(string sql)
        { 
		     
		    try
            {
			    string sqlstr="select * from  CBOF_BASE_DATA  where " +sql; 
                var data=  db.Database.SqlQuery<CBOF_BASE_DATA>(sqlstr).ToList();				
				Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();			
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		
		 /// <summary>
        /// 主表联查其他表 sql
        /// </summary>
        /// <param name="sql">完整的sql语句</param>
        /// <returns>message消息</returns>	
		[Route("QueryJoin")]
        [HttpGet]
		public Message<CBOF_BASE_DATA> QueryJion(string sql)
        { 
		     
		    try
            {			   
                var data= db.Database.SqlQuery<CBOF_BASE_DATA>(sql).ToList();				
				Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();				
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        /// 修改操作 sql
        /// </summary>
        /// <param name="sql">完整的sql语句</param>
        /// <returns>message消息</returns>	
		[Route("update")]
        [HttpGet]
		public  Message<CBOF_BASE_DATA> update(string sql)
        { 
		     
		    try
            {			   
                db.Database.ExecuteSqlCommand(sql);				  
				Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();				
				ms.success="true";
				ms.message="操作成功";
                return ms;				
            }
            catch (Exception ex)
            {  
			    Message<CBOF_BASE_DATA> ms=new Message<CBOF_BASE_DATA>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

				
    }   
    [RoutePrefix("api/S_RESOURCE_T_Api")]
	public partial class S_RESOURCE_T_ApiController :ApiController
    {	     
	
	    Entities db=new Entities();
		
	

	    /// <summary>
        /// 加载单表数据
        /// </summary>
		/// <returns>message消息</returns> 
		[Route("Load")]
        [HttpGet]
		//序列化类型为“System.Data.Entity.DynamicProxies.Photos....这个会的对象时检测到循环引用。
		// db.Configuration.LazyLoadingEnabled = false;
		//  db.Configuration.ProxyCreationEnabled = false;
		//因为这个表和另一个表是有一对多关系的,当序列化表1的时候,会找到和另一个表2关联的字段,就会到另一个表2中序列化,
		//然后另一个表2中也有一个字段和表1相关联.这样.序列化就会发生这种错误
		public   Message<S_RESOURCE_T> Load()
        { 		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.S_RESOURCE_T.AsNoTracking().ToList(); 
				Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();
				//ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        /// 单表有效值查询
        /// </summary>
        /// <returns>message消息</returns>
		[Route("LoadValid")]
        [HttpGet]
		public  Message<S_RESOURCE_T> LoadValid()
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data= db.S_RESOURCE_T.AsNoTracking().Where(o=>o.DR=="0").ToList(); 
				Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();
				//ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
			    Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
                
            }
		                    
        }

		 /// <summary>
        ///  分页查询
        /// </summary>
        /// <param name="limit">一页多少条</param>
        /// <param name="offset">第几页</param>
        /// <returns>message消息</returns>

		[Route("LoadPage")]
        [HttpGet]
		//返回类型是Message 报错“ObjectContent`1”类型未能序列化内容类型“application/xml; charset=utf-8”的响应正文。 
		public Message<S_RESOURCE_T> LoadPage(int limit,int offset)
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.S_RESOURCE_T.AsNoTracking().OrderBy(o=>o.TS).Skip(offset).Take(limit).ToList();
				var count=db.S_RESOURCE_T.Count();
				Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();
				ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
			    Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
               
            }
		                    
        }


		 /// <summary>
        ///  根据id值查询单条数据
        /// </summary>
        /// <param name="id">主键id值</param>
        /// <returns>message消息</returns>		
		[Route("Find")]
        [HttpGet]
		public  Message<S_RESOURCE_T> Find(string id)
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
			    List<S_RESOURCE_T> list=new List<S_RESOURCE_T>();
			    var data=db.S_RESOURCE_T.Find(id);
				Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();				
				ms.row=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        ///  根据id主键查询并删除
        /// </summary>
        /// <param name="id">主键id值</param>
        /// <returns>message消息</returns>		
		[Route("FindDelete")]
        [HttpGet]
		public  Message<S_RESOURCE_T> FindDelete(string id)
        { 		     
		    try
            {	
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;				
				Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();
			    var data=db.S_RESOURCE_T.Find(id);
				if(data!=null)
				{
				  db.S_RESOURCE_T.Remove(data);
				  db.SaveChangesAsync();
				}							
				 ms.success="true";
				 ms.message="操作成功";
                 return ms;
               
            }
            catch (Exception ex)
            {
                Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        ///  单表时间查询 大于开始时间 小于结束时间
        /// </summary>
        /// <param name="begin">开始时间</param>
		 /// <param name="begin">结束时间</param>
        /// <returns>message消息</returns>	
		[Route("LoadByTs")]
        [HttpGet]
		public  Message<S_RESOURCE_T> LoadByTs(string begin,string end)
        { 
		     
		    try
            {   
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.S_RESOURCE_T.AsNoTracking().Where(o=>o.TS.CompareTo(begin)>=0&&o.TS.CompareTo(end)<=0).ToList(); 
				Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();				
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }


		 /// <summary>
        ///  单表条件查询 sql
        /// </summary>
        /// <param name="sql">查询条件</param>
        /// <returns>message消息</returns>	
		[Route("Query")]
        [HttpGet]
		public  Message<S_RESOURCE_T> Query(string sql)
        { 
		     
		    try
            {
			    string sqlstr="select * from  S_RESOURCE_T  where " +sql; 
                var data=  db.Database.SqlQuery<S_RESOURCE_T>(sqlstr).ToList();				
				Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();			
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		
		 /// <summary>
        /// 主表联查其他表 sql
        /// </summary>
        /// <param name="sql">完整的sql语句</param>
        /// <returns>message消息</returns>	
		[Route("QueryJoin")]
        [HttpGet]
		public Message<S_RESOURCE_T> QueryJion(string sql)
        { 
		     
		    try
            {			   
                var data= db.Database.SqlQuery<S_RESOURCE_T>(sql).ToList();				
				Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();				
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        /// 修改操作 sql
        /// </summary>
        /// <param name="sql">完整的sql语句</param>
        /// <returns>message消息</returns>	
		[Route("update")]
        [HttpGet]
		public  Message<S_RESOURCE_T> update(string sql)
        { 
		     
		    try
            {			   
                db.Database.ExecuteSqlCommand(sql);				  
				Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();				
				ms.success="true";
				ms.message="操作成功";
                return ms;				
            }
            catch (Exception ex)
            {  
			    Message<S_RESOURCE_T> ms=new Message<S_RESOURCE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

				
    }   
    [RoutePrefix("api/S_RESOURCEROLE_T_Api")]
	public partial class S_RESOURCEROLE_T_ApiController :ApiController
    {	     
	
	    Entities db=new Entities();
		
	

	    /// <summary>
        /// 加载单表数据
        /// </summary>
		/// <returns>message消息</returns> 
		[Route("Load")]
        [HttpGet]
		//序列化类型为“System.Data.Entity.DynamicProxies.Photos....这个会的对象时检测到循环引用。
		// db.Configuration.LazyLoadingEnabled = false;
		//  db.Configuration.ProxyCreationEnabled = false;
		//因为这个表和另一个表是有一对多关系的,当序列化表1的时候,会找到和另一个表2关联的字段,就会到另一个表2中序列化,
		//然后另一个表2中也有一个字段和表1相关联.这样.序列化就会发生这种错误
		public   Message<S_RESOURCEROLE_T> Load()
        { 		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.S_RESOURCEROLE_T.AsNoTracking().ToList(); 
				Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();
				//ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        /// 单表有效值查询
        /// </summary>
        /// <returns>message消息</returns>
		[Route("LoadValid")]
        [HttpGet]
		public  Message<S_RESOURCEROLE_T> LoadValid()
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data= db.S_RESOURCEROLE_T.AsNoTracking().Where(o=>o.DR=="0").ToList(); 
				Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();
				//ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
			    Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
                
            }
		                    
        }

		 /// <summary>
        ///  分页查询
        /// </summary>
        /// <param name="limit">一页多少条</param>
        /// <param name="offset">第几页</param>
        /// <returns>message消息</returns>

		[Route("LoadPage")]
        [HttpGet]
		//返回类型是Message 报错“ObjectContent`1”类型未能序列化内容类型“application/xml; charset=utf-8”的响应正文。 
		public Message<S_RESOURCEROLE_T> LoadPage(int limit,int offset)
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.S_RESOURCEROLE_T.AsNoTracking().OrderBy(o=>o.TS).Skip(offset).Take(limit).ToList();
				var count=db.S_RESOURCEROLE_T.Count();
				Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();
				ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
			    Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
               
            }
		                    
        }


		 /// <summary>
        ///  根据id值查询单条数据
        /// </summary>
        /// <param name="id">主键id值</param>
        /// <returns>message消息</returns>		
		[Route("Find")]
        [HttpGet]
		public  Message<S_RESOURCEROLE_T> Find(string id)
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
			    List<S_RESOURCEROLE_T> list=new List<S_RESOURCEROLE_T>();
			    var data=db.S_RESOURCEROLE_T.Find(id);
				Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();				
				ms.row=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        ///  根据id主键查询并删除
        /// </summary>
        /// <param name="id">主键id值</param>
        /// <returns>message消息</returns>		
		[Route("FindDelete")]
        [HttpGet]
		public  Message<S_RESOURCEROLE_T> FindDelete(string id)
        { 		     
		    try
            {	
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;				
				Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();
			    var data=db.S_RESOURCEROLE_T.Find(id);
				if(data!=null)
				{
				  db.S_RESOURCEROLE_T.Remove(data);
				  db.SaveChangesAsync();
				}							
				 ms.success="true";
				 ms.message="操作成功";
                 return ms;
               
            }
            catch (Exception ex)
            {
                Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        ///  单表时间查询 大于开始时间 小于结束时间
        /// </summary>
        /// <param name="begin">开始时间</param>
		 /// <param name="begin">结束时间</param>
        /// <returns>message消息</returns>	
		[Route("LoadByTs")]
        [HttpGet]
		public  Message<S_RESOURCEROLE_T> LoadByTs(string begin,string end)
        { 
		     
		    try
            {   
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.S_RESOURCEROLE_T.AsNoTracking().Where(o=>o.TS.CompareTo(begin)>=0&&o.TS.CompareTo(end)<=0).ToList(); 
				Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();				
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }


		 /// <summary>
        ///  单表条件查询 sql
        /// </summary>
        /// <param name="sql">查询条件</param>
        /// <returns>message消息</returns>	
		[Route("Query")]
        [HttpGet]
		public  Message<S_RESOURCEROLE_T> Query(string sql)
        { 
		     
		    try
            {
			    string sqlstr="select * from  S_RESOURCEROLE_T  where " +sql; 
                var data=  db.Database.SqlQuery<S_RESOURCEROLE_T>(sqlstr).ToList();				
				Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();			
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		
		 /// <summary>
        /// 主表联查其他表 sql
        /// </summary>
        /// <param name="sql">完整的sql语句</param>
        /// <returns>message消息</returns>	
		[Route("QueryJoin")]
        [HttpGet]
		public Message<S_RESOURCEROLE_T> QueryJion(string sql)
        { 
		     
		    try
            {			   
                var data= db.Database.SqlQuery<S_RESOURCEROLE_T>(sql).ToList();				
				Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();				
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        /// 修改操作 sql
        /// </summary>
        /// <param name="sql">完整的sql语句</param>
        /// <returns>message消息</returns>	
		[Route("update")]
        [HttpGet]
		public  Message<S_RESOURCEROLE_T> update(string sql)
        { 
		     
		    try
            {			   
                db.Database.ExecuteSqlCommand(sql);				  
				Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();				
				ms.success="true";
				ms.message="操作成功";
                return ms;				
            }
            catch (Exception ex)
            {  
			    Message<S_RESOURCEROLE_T> ms=new Message<S_RESOURCEROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

				
    }   
    [RoutePrefix("api/S_ROLE_T_Api")]
	public partial class S_ROLE_T_ApiController :ApiController
    {	     
	
	    Entities db=new Entities();
		
	

	    /// <summary>
        /// 加载单表数据
        /// </summary>
		/// <returns>message消息</returns> 
		[Route("Load")]
        [HttpGet]
		//序列化类型为“System.Data.Entity.DynamicProxies.Photos....这个会的对象时检测到循环引用。
		// db.Configuration.LazyLoadingEnabled = false;
		//  db.Configuration.ProxyCreationEnabled = false;
		//因为这个表和另一个表是有一对多关系的,当序列化表1的时候,会找到和另一个表2关联的字段,就会到另一个表2中序列化,
		//然后另一个表2中也有一个字段和表1相关联.这样.序列化就会发生这种错误
		public   Message<S_ROLE_T> Load()
        { 		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.S_ROLE_T.AsNoTracking().ToList(); 
				Message<S_ROLE_T> ms=new Message<S_ROLE_T>();
				//ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_ROLE_T> ms=new Message<S_ROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        /// 单表有效值查询
        /// </summary>
        /// <returns>message消息</returns>
		[Route("LoadValid")]
        [HttpGet]
		public  Message<S_ROLE_T> LoadValid()
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data= db.S_ROLE_T.AsNoTracking().Where(o=>o.DR=="0").ToList(); 
				Message<S_ROLE_T> ms=new Message<S_ROLE_T>();
				//ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
			    Message<S_ROLE_T> ms=new Message<S_ROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
                
            }
		                    
        }

		 /// <summary>
        ///  分页查询
        /// </summary>
        /// <param name="limit">一页多少条</param>
        /// <param name="offset">第几页</param>
        /// <returns>message消息</returns>

		[Route("LoadPage")]
        [HttpGet]
		//返回类型是Message 报错“ObjectContent`1”类型未能序列化内容类型“application/xml; charset=utf-8”的响应正文。 
		public Message<S_ROLE_T> LoadPage(int limit,int offset)
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.S_ROLE_T.AsNoTracking().OrderBy(o=>o.TS).Skip(offset).Take(limit).ToList();
				var count=db.S_ROLE_T.Count();
				Message<S_ROLE_T> ms=new Message<S_ROLE_T>();
				ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
			    Message<S_ROLE_T> ms=new Message<S_ROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
               
            }
		                    
        }


		 /// <summary>
        ///  根据id值查询单条数据
        /// </summary>
        /// <param name="id">主键id值</param>
        /// <returns>message消息</returns>		
		[Route("Find")]
        [HttpGet]
		public  Message<S_ROLE_T> Find(string id)
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
			    List<S_ROLE_T> list=new List<S_ROLE_T>();
			    var data=db.S_ROLE_T.Find(id);
				Message<S_ROLE_T> ms=new Message<S_ROLE_T>();				
				ms.row=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_ROLE_T> ms=new Message<S_ROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        ///  根据id主键查询并删除
        /// </summary>
        /// <param name="id">主键id值</param>
        /// <returns>message消息</returns>		
		[Route("FindDelete")]
        [HttpGet]
		public  Message<S_ROLE_T> FindDelete(string id)
        { 		     
		    try
            {	
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;				
				Message<S_ROLE_T> ms=new Message<S_ROLE_T>();
			    var data=db.S_ROLE_T.Find(id);
				if(data!=null)
				{
				  db.S_ROLE_T.Remove(data);
				  db.SaveChangesAsync();
				}							
				 ms.success="true";
				 ms.message="操作成功";
                 return ms;
               
            }
            catch (Exception ex)
            {
                Message<S_ROLE_T> ms=new Message<S_ROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        ///  单表时间查询 大于开始时间 小于结束时间
        /// </summary>
        /// <param name="begin">开始时间</param>
		 /// <param name="begin">结束时间</param>
        /// <returns>message消息</returns>	
		[Route("LoadByTs")]
        [HttpGet]
		public  Message<S_ROLE_T> LoadByTs(string begin,string end)
        { 
		     
		    try
            {   
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.S_ROLE_T.AsNoTracking().Where(o=>o.TS.CompareTo(begin)>=0&&o.TS.CompareTo(end)<=0).ToList(); 
				Message<S_ROLE_T> ms=new Message<S_ROLE_T>();				
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_ROLE_T> ms=new Message<S_ROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }


		 /// <summary>
        ///  单表条件查询 sql
        /// </summary>
        /// <param name="sql">查询条件</param>
        /// <returns>message消息</returns>	
		[Route("Query")]
        [HttpGet]
		public  Message<S_ROLE_T> Query(string sql)
        { 
		     
		    try
            {
			    string sqlstr="select * from  S_ROLE_T  where " +sql; 
                var data=  db.Database.SqlQuery<S_ROLE_T>(sqlstr).ToList();				
				Message<S_ROLE_T> ms=new Message<S_ROLE_T>();			
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_ROLE_T> ms=new Message<S_ROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		
		 /// <summary>
        /// 主表联查其他表 sql
        /// </summary>
        /// <param name="sql">完整的sql语句</param>
        /// <returns>message消息</returns>	
		[Route("QueryJoin")]
        [HttpGet]
		public Message<S_ROLE_T> QueryJion(string sql)
        { 
		     
		    try
            {			   
                var data= db.Database.SqlQuery<S_ROLE_T>(sql).ToList();				
				Message<S_ROLE_T> ms=new Message<S_ROLE_T>();				
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_ROLE_T> ms=new Message<S_ROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        /// 修改操作 sql
        /// </summary>
        /// <param name="sql">完整的sql语句</param>
        /// <returns>message消息</returns>	
		[Route("update")]
        [HttpGet]
		public  Message<S_ROLE_T> update(string sql)
        { 
		     
		    try
            {			   
                db.Database.ExecuteSqlCommand(sql);				  
				Message<S_ROLE_T> ms=new Message<S_ROLE_T>();				
				ms.success="true";
				ms.message="操作成功";
                return ms;				
            }
            catch (Exception ex)
            {  
			    Message<S_ROLE_T> ms=new Message<S_ROLE_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

				
    }   
    [RoutePrefix("api/S_ROLEUSER_T_Api")]
	public partial class S_ROLEUSER_T_ApiController :ApiController
    {	     
	
	    Entities db=new Entities();
		
	

	    /// <summary>
        /// 加载单表数据
        /// </summary>
		/// <returns>message消息</returns> 
		[Route("Load")]
        [HttpGet]
		//序列化类型为“System.Data.Entity.DynamicProxies.Photos....这个会的对象时检测到循环引用。
		// db.Configuration.LazyLoadingEnabled = false;
		//  db.Configuration.ProxyCreationEnabled = false;
		//因为这个表和另一个表是有一对多关系的,当序列化表1的时候,会找到和另一个表2关联的字段,就会到另一个表2中序列化,
		//然后另一个表2中也有一个字段和表1相关联.这样.序列化就会发生这种错误
		public   Message<S_ROLEUSER_T> Load()
        { 		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.S_ROLEUSER_T.AsNoTracking().ToList(); 
				Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();
				//ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        /// 单表有效值查询
        /// </summary>
        /// <returns>message消息</returns>
		[Route("LoadValid")]
        [HttpGet]
		public  Message<S_ROLEUSER_T> LoadValid()
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data= db.S_ROLEUSER_T.AsNoTracking().Where(o=>o.DR=="0").ToList(); 
				Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();
				//ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
			    Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
                
            }
		                    
        }

		 /// <summary>
        ///  分页查询
        /// </summary>
        /// <param name="limit">一页多少条</param>
        /// <param name="offset">第几页</param>
        /// <returns>message消息</returns>

		[Route("LoadPage")]
        [HttpGet]
		//返回类型是Message 报错“ObjectContent`1”类型未能序列化内容类型“application/xml; charset=utf-8”的响应正文。 
		public Message<S_ROLEUSER_T> LoadPage(int limit,int offset)
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.S_ROLEUSER_T.AsNoTracking().OrderBy(o=>o.TS).Skip(offset).Take(limit).ToList();
				var count=db.S_ROLEUSER_T.Count();
				Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();
				ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
			    Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
               
            }
		                    
        }


		 /// <summary>
        ///  根据id值查询单条数据
        /// </summary>
        /// <param name="id">主键id值</param>
        /// <returns>message消息</returns>		
		[Route("Find")]
        [HttpGet]
		public  Message<S_ROLEUSER_T> Find(string id)
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
			    List<S_ROLEUSER_T> list=new List<S_ROLEUSER_T>();
			    var data=db.S_ROLEUSER_T.Find(id);
				Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();				
				ms.row=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        ///  根据id主键查询并删除
        /// </summary>
        /// <param name="id">主键id值</param>
        /// <returns>message消息</returns>		
		[Route("FindDelete")]
        [HttpGet]
		public  Message<S_ROLEUSER_T> FindDelete(string id)
        { 		     
		    try
            {	
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;				
				Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();
			    var data=db.S_ROLEUSER_T.Find(id);
				if(data!=null)
				{
				  db.S_ROLEUSER_T.Remove(data);
				  db.SaveChangesAsync();
				}							
				 ms.success="true";
				 ms.message="操作成功";
                 return ms;
               
            }
            catch (Exception ex)
            {
                Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        ///  单表时间查询 大于开始时间 小于结束时间
        /// </summary>
        /// <param name="begin">开始时间</param>
		 /// <param name="begin">结束时间</param>
        /// <returns>message消息</returns>	
		[Route("LoadByTs")]
        [HttpGet]
		public  Message<S_ROLEUSER_T> LoadByTs(string begin,string end)
        { 
		     
		    try
            {   
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.S_ROLEUSER_T.AsNoTracking().Where(o=>o.TS.CompareTo(begin)>=0&&o.TS.CompareTo(end)<=0).ToList(); 
				Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();				
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }


		 /// <summary>
        ///  单表条件查询 sql
        /// </summary>
        /// <param name="sql">查询条件</param>
        /// <returns>message消息</returns>	
		[Route("Query")]
        [HttpGet]
		public  Message<S_ROLEUSER_T> Query(string sql)
        { 
		     
		    try
            {
			    string sqlstr="select * from  S_ROLEUSER_T  where " +sql; 
                var data=  db.Database.SqlQuery<S_ROLEUSER_T>(sqlstr).ToList();				
				Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();			
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		
		 /// <summary>
        /// 主表联查其他表 sql
        /// </summary>
        /// <param name="sql">完整的sql语句</param>
        /// <returns>message消息</returns>	
		[Route("QueryJoin")]
        [HttpGet]
		public Message<S_ROLEUSER_T> QueryJion(string sql)
        { 
		     
		    try
            {			   
                var data= db.Database.SqlQuery<S_ROLEUSER_T>(sql).ToList();				
				Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();				
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        /// 修改操作 sql
        /// </summary>
        /// <param name="sql">完整的sql语句</param>
        /// <returns>message消息</returns>	
		[Route("update")]
        [HttpGet]
		public  Message<S_ROLEUSER_T> update(string sql)
        { 
		     
		    try
            {			   
                db.Database.ExecuteSqlCommand(sql);				  
				Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();				
				ms.success="true";
				ms.message="操作成功";
                return ms;				
            }
            catch (Exception ex)
            {  
			    Message<S_ROLEUSER_T> ms=new Message<S_ROLEUSER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

				
    }   
    [RoutePrefix("api/S_USER_T_Api")]
	public partial class S_USER_T_ApiController :ApiController
    {	     
	
	    Entities db=new Entities();
		
	

	    /// <summary>
        /// 加载单表数据
        /// </summary>
		/// <returns>message消息</returns> 
		[Route("Load")]
        [HttpGet]
		//序列化类型为“System.Data.Entity.DynamicProxies.Photos....这个会的对象时检测到循环引用。
		// db.Configuration.LazyLoadingEnabled = false;
		//  db.Configuration.ProxyCreationEnabled = false;
		//因为这个表和另一个表是有一对多关系的,当序列化表1的时候,会找到和另一个表2关联的字段,就会到另一个表2中序列化,
		//然后另一个表2中也有一个字段和表1相关联.这样.序列化就会发生这种错误
		public   Message<S_USER_T> Load()
        { 		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.S_USER_T.AsNoTracking().ToList(); 
				Message<S_USER_T> ms=new Message<S_USER_T>();
				//ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_USER_T> ms=new Message<S_USER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        /// 单表有效值查询
        /// </summary>
        /// <returns>message消息</returns>
		[Route("LoadValid")]
        [HttpGet]
		public  Message<S_USER_T> LoadValid()
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data= db.S_USER_T.AsNoTracking().Where(o=>o.DR=="0").ToList(); 
				Message<S_USER_T> ms=new Message<S_USER_T>();
				//ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
			    Message<S_USER_T> ms=new Message<S_USER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
                
            }
		                    
        }

		 /// <summary>
        ///  分页查询
        /// </summary>
        /// <param name="limit">一页多少条</param>
        /// <param name="offset">第几页</param>
        /// <returns>message消息</returns>

		[Route("LoadPage")]
        [HttpGet]
		//返回类型是Message 报错“ObjectContent`1”类型未能序列化内容类型“application/xml; charset=utf-8”的响应正文。 
		public Message<S_USER_T> LoadPage(int limit,int offset)
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.S_USER_T.AsNoTracking().OrderBy(o=>o.TS).Skip(offset).Take(limit).ToList();
				var count=db.S_USER_T.Count();
				Message<S_USER_T> ms=new Message<S_USER_T>();
				ms.total=count;
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
			    Message<S_USER_T> ms=new Message<S_USER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
               
            }
		                    
        }


		 /// <summary>
        ///  根据id值查询单条数据
        /// </summary>
        /// <param name="id">主键id值</param>
        /// <returns>message消息</returns>		
		[Route("Find")]
        [HttpGet]
		public  Message<S_USER_T> Find(string id)
        { 
		     
		    try
            {
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
			    List<S_USER_T> list=new List<S_USER_T>();
			    var data=db.S_USER_T.Find(id);
				Message<S_USER_T> ms=new Message<S_USER_T>();				
				ms.row=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_USER_T> ms=new Message<S_USER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        ///  根据id主键查询并删除
        /// </summary>
        /// <param name="id">主键id值</param>
        /// <returns>message消息</returns>		
		[Route("FindDelete")]
        [HttpGet]
		public  Message<S_USER_T> FindDelete(string id)
        { 		     
		    try
            {	
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;				
				Message<S_USER_T> ms=new Message<S_USER_T>();
			    var data=db.S_USER_T.Find(id);
				if(data!=null)
				{
				  db.S_USER_T.Remove(data);
				  db.SaveChangesAsync();
				}							
				 ms.success="true";
				 ms.message="操作成功";
                 return ms;
               
            }
            catch (Exception ex)
            {
                Message<S_USER_T> ms=new Message<S_USER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        ///  单表时间查询 大于开始时间 小于结束时间
        /// </summary>
        /// <param name="begin">开始时间</param>
		 /// <param name="begin">结束时间</param>
        /// <returns>message消息</returns>	
		[Route("LoadByTs")]
        [HttpGet]
		public  Message<S_USER_T> LoadByTs(string begin,string end)
        { 
		     
		    try
            {   
			    db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                var data=  db.S_USER_T.AsNoTracking().Where(o=>o.TS.CompareTo(begin)>=0&&o.TS.CompareTo(end)<=0).ToList(); 
				Message<S_USER_T> ms=new Message<S_USER_T>();				
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_USER_T> ms=new Message<S_USER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }


		 /// <summary>
        ///  单表条件查询 sql
        /// </summary>
        /// <param name="sql">查询条件</param>
        /// <returns>message消息</returns>	
		[Route("Query")]
        [HttpGet]
		public  Message<S_USER_T> Query(string sql)
        { 
		     
		    try
            {
			    string sqlstr="select * from  S_USER_T  where " +sql; 
                var data=  db.Database.SqlQuery<S_USER_T>(sqlstr).ToList();				
				Message<S_USER_T> ms=new Message<S_USER_T>();			
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_USER_T> ms=new Message<S_USER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		
		 /// <summary>
        /// 主表联查其他表 sql
        /// </summary>
        /// <param name="sql">完整的sql语句</param>
        /// <returns>message消息</returns>	
		[Route("QueryJoin")]
        [HttpGet]
		public Message<S_USER_T> QueryJion(string sql)
        { 
		     
		    try
            {			   
                var data= db.Database.SqlQuery<S_USER_T>(sql).ToList();				
				Message<S_USER_T> ms=new Message<S_USER_T>();				
				ms.rows=data;
				ms.success="true";
				ms.message="操作成功";
                return ms;
            }
            catch (Exception ex)
            {
                Message<S_USER_T> ms=new Message<S_USER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

		 /// <summary>
        /// 修改操作 sql
        /// </summary>
        /// <param name="sql">完整的sql语句</param>
        /// <returns>message消息</returns>	
		[Route("update")]
        [HttpGet]
		public  Message<S_USER_T> update(string sql)
        { 
		     
		    try
            {			   
                db.Database.ExecuteSqlCommand(sql);				  
				Message<S_USER_T> ms=new Message<S_USER_T>();				
				ms.success="true";
				ms.message="操作成功";
                return ms;				
            }
            catch (Exception ex)
            {  
			    Message<S_USER_T> ms=new Message<S_USER_T>();
				ms.success="false";
				ms.message="操作失败，"+ex.Message;
                return ms;
            }
		                    
        }

				
    }   
	

 public class Message<T> where T : class, new()
    {
	    /// <summary>
        /// 提示信息，操作成功或失败
        /// </summary>
        public string message { get; set; }
	    /// <summary>
        /// true或者false，
        /// </summary>
        public string success { get; set; }
		 /// <summary>
        /// list<T> 数据列表
        /// </summary>
        public List<T> rows { get; set; }
		 /// <summary>
        /// T  单条数据
        /// </summary>
        public T row { get; set; }
		 /// <summary>
        /// 总数
        /// </summary>
        public Int64 total { get; set; }
    }
}