import{_ as t,c as e,o as l,a}from"./app.149613ba.js";const b='{"title":"ServiceNow.Core","description":"","frontmatter":{},"headers":[{"level":3,"title":"SNow.Core","slug":"snow-core"},{"level":2,"title":"Table<T> Class","slug":"table-t-class"}],"relativePath":"Table_T_.md","lastUpdated":1642480795538}',s={},o=a(`<h4 id="servicenow-core" tabindex="-1"><a href="./baseindex.html" title="baseindex">ServiceNow.Core</a> <a class="header-anchor" href="#servicenow-core" aria-hidden="true">#</a></h4><h3 id="snow-core" tabindex="-1"><a href="./SNow_Core.html" title="SNow.Core">SNow.Core</a> <a class="header-anchor" href="#snow-core" aria-hidden="true">#</a></h3><h2 id="table-t-class" tabindex="-1">Table&lt;T&gt; Class <a class="header-anchor" href="#table-t-class" aria-hidden="true">#</a></h2><div class="language-csharp"><pre><code><span class="token keyword">public</span> <span class="token keyword">class</span> <span class="token class-name">Table<span class="token punctuation">&lt;</span>T<span class="token punctuation">&gt;</span></span> <span class="token punctuation">:</span> <span class="token type-list"><span class="token class-name">SNow<span class="token punctuation">.</span>Core<span class="token punctuation">.</span>TableBase</span><span class="token punctuation">,</span>
<span class="token class-name">SNow<span class="token punctuation">.</span>Core<span class="token punctuation">.</span>ITable<span class="token punctuation">&lt;</span>T<span class="token punctuation">&gt;</span></span></span>
    <span class="token keyword">where</span> <span class="token class-name">T</span> <span class="token punctuation">:</span> <span class="token type-list"><span class="token class-name">SNow<span class="token punctuation">.</span>Core<span class="token punctuation">.</span>Models<span class="token punctuation">.</span>ServiceNowBaseModel</span></span>
</code></pre></div><h4 id="type-parameters" tabindex="-1">Type parameters <a class="header-anchor" href="#type-parameters" aria-hidden="true">#</a></h4><p><a name="SNow_Core_Table_T__T"></a><code>T</code></p><p>Inheritance <a href="https://docs.microsoft.com/en-us/dotnet/api/System.Object" title="System.Object" target="_blank" rel="noopener noreferrer">System.Object</a> \u{1F852} <a href="./TableBase.html" title="SNow.Core.TableBase">TableBase</a> \u{1F852} Table&lt;T&gt;</p><p>Implements <a href="./ITable_T_.html" title="SNow.Core.ITable&lt;T&gt;">SNow.Core.ITable&lt;</a><a href="./Table_T_.html#SNow_Core_Table_T__T" title="SNow.Core.Table&lt;T&gt;.T">T</a><a href="./ITable_T_.html" title="SNow.Core.ITable&lt;T&gt;">&gt;</a></p><table><thead><tr><th style="text-align:left;">Constructors</th><th style="text-align:left;"></th></tr></thead><tbody><tr><td style="text-align:left;"><a href="./Table_T__Table(IServiceNow_ILogger).html" title="SNow.Core.Table&lt;T&gt;.Table(SNow.Core.IServiceNow, Microsoft.Extensions.Logging.ILogger)">Table(IServiceNow, ILogger)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./Table_T__Table(IServiceNow_string_ILogger).html" title="SNow.Core.Table&lt;T&gt;.Table(SNow.Core.IServiceNow, string, Microsoft.Extensions.Logging.ILogger)">Table(IServiceNow, string, ILogger)</a></td><td style="text-align:left;"></td></tr></tbody></table><table><thead><tr><th style="text-align:left;">Fields</th><th style="text-align:left;"></th></tr></thead><tbody><tr><td style="text-align:left;"><a href="./Table_T___where.html" title="SNow.Core.Table&lt;T&gt;._where">_where</a></td><td style="text-align:left;"></td></tr></tbody></table><table><thead><tr><th style="text-align:left;">Methods</th><th style="text-align:left;"></th></tr></thead><tbody><tr><td style="text-align:left;"><a href="./Table_T__Limit(int).html" title="SNow.Core.Table&lt;T&gt;.Limit(int)">Limit(int)</a></td><td style="text-align:left;">The maximum number of results returned per page (default: 10,000)</td></tr><tr><td style="text-align:left;"><a href="./Table_T__OrderBy(Expression_Func_T_object__).html" title="SNow.Core.Table&lt;T&gt;.OrderBy(System.Linq.Expressions.Expression&lt;System.Func&lt;T,object&gt;&gt;)">OrderBy(Expression&lt;Func&lt;T,object&gt;&gt;)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./Table_T__OrderByDesc(Expression_Func_T_object__).html" title="SNow.Core.Table&lt;T&gt;.OrderByDesc(System.Linq.Expressions.Expression&lt;System.Func&lt;T,object&gt;&gt;)">OrderByDesc(Expression&lt;Func&lt;T,object&gt;&gt;)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./Table_T__Select(Expression_Func_T_object____).html" title="SNow.Core.Table&lt;T&gt;.Select(System.Linq.Expressions.Expression&lt;System.Func&lt;T,object&gt;&gt;[])">Select(Expression&lt;Func&lt;T,object&gt;&gt;[])</a></td><td style="text-align:left;">List of properties to return, impacts the size of the response</td></tr><tr><td style="text-align:left;"><a href="./Table_T__SetHeaders(List_KeyValuePair_string_string__).html" title="SNow.Core.Table&lt;T&gt;.SetHeaders(System.Collections.Generic.List&lt;System.Collections.Generic.KeyValuePair&lt;string,string&gt;&gt;)">SetHeaders(List&lt;KeyValuePair&lt;string,string&gt;&gt;)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./Table_T__ToListAsync(Nullable_int_).html" title="SNow.Core.Table&lt;T&gt;.ToListAsync(System.Nullable&lt;int&gt;)">ToListAsync(Nullable&lt;int&gt;)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./Table_T__Where(Expression_Func_T_bool__).html" title="SNow.Core.Table&lt;T&gt;.Where(System.Linq.Expressions.Expression&lt;System.Func&lt;T,bool&gt;&gt;)">Where(Expression&lt;Func&lt;T,bool&gt;&gt;)</a></td><td style="text-align:left;">Set query parameters to the API request using Where clause.<br> Don&#39;t use it together with &quot;WithQuery&quot;</td></tr><tr><td style="text-align:left;"><a href="./Table_T__WithQuery(Expression_Func_T_string__).html" title="SNow.Core.Table&lt;T&gt;.WithQuery(System.Linq.Expressions.Expression&lt;System.Func&lt;T,string&gt;&gt;)">WithQuery(Expression&lt;Func&lt;T,string&gt;&gt;)</a></td><td style="text-align:left;">The query must have only those operators and, or, like, =, !=, startsWith, endsWith see <a href="https://docs.servicenow.com/bundle/rome-application-development/page/integrate/inbound-rest/concept/c_RESTAPI.html" title="https://docs.servicenow.com/bundle/rome-application-development/page/integrate/inbound-rest/concept/c_RESTAPI.html" target="_blank" rel="noopener noreferrer">SN Rest Operators</a></td></tr></tbody></table><table><thead><tr><th style="text-align:left;">Explicit Interface Implementations</th><th style="text-align:left;"></th></tr></thead><tbody><tr><td style="text-align:left;"><a href="./Table_T__SNow_Core_ITable_T__Create(object).html" title="SNow.Core.Table&lt;T&gt;.SNow.Core.ITable&lt;T&gt;.Create(object)">SNow.Core.ITable&lt;T&gt;.Create(object)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./Table_T__SNow_Core_ITable_T__Delete(Guid).html" title="SNow.Core.Table&lt;T&gt;.SNow.Core.ITable&lt;T&gt;.Delete(System.Guid)">SNow.Core.ITable&lt;T&gt;.Delete(Guid)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./Table_T__SNow_Core_ITable_T__GetByIdAsync(Guid).html" title="SNow.Core.Table&lt;T&gt;.SNow.Core.ITable&lt;T&gt;.GetByIdAsync(System.Guid)">SNow.Core.ITable&lt;T&gt;.GetByIdAsync(Guid)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./Table_T__SNow_Core_ITable_T__Update(Nullable_Guid__object_bool).html" title="SNow.Core.Table&lt;T&gt;.SNow.Core.ITable&lt;T&gt;.Update(System.Nullable&lt;System.Guid&gt;, object, bool)">SNow.Core.ITable&lt;T&gt;.Update(Nullable&lt;Guid&gt;, object, bool)</a></td><td style="text-align:left;"></td></tr></tbody></table>`,12),n=[o];function r(i,c,d,_,T,p){return l(),e("div",null,n)}var h=t(s,[["render",r]]);export{b as __pageData,h as default};
