import{_ as t,c as e,o as a,d as l}from"./app.ac5b6d71.js";const y='{"title":"ServiceNow.Core","description":"","frontmatter":{},"headers":[{"level":3,"title":"SNow.Core","slug":"snow-core"},{"level":2,"title":"Table Class","slug":"table-class"}],"relativePath":"auto/Table.md","lastUpdated":1649560581645}',s={},r=l(`<h4 id="servicenow-core" tabindex="-1"><a href="./" title="index">ServiceNow.Core</a> <a class="header-anchor" href="#servicenow-core" aria-hidden="true">#</a></h4><h3 id="snow-core" tabindex="-1"><a href="./SNow_Core.html" title="SNow.Core">SNow.Core</a> <a class="header-anchor" href="#snow-core" aria-hidden="true">#</a></h3><h2 id="table-class" tabindex="-1">Table Class <a class="header-anchor" href="#table-class" aria-hidden="true">#</a></h2><div class="language-csharp"><pre><code><span class="token keyword">public</span> <span class="token keyword">class</span> <span class="token class-name">Table</span> <span class="token punctuation">:</span> <span class="token type-list"><span class="token class-name">SNow<span class="token punctuation">.</span>Core<span class="token punctuation">.</span>TableBase</span><span class="token punctuation">,</span>
<span class="token class-name">SNow<span class="token punctuation">.</span>Core<span class="token punctuation">.</span>ITable</span></span>
</code></pre></div><p>Inheritance <a href="https://docs.microsoft.com/en-us/dotnet/api/System.Object" title="System.Object" target="_blank" rel="noopener noreferrer">System.Object</a> \u{1F852} <a href="./TableBase.html" title="SNow.Core.TableBase">TableBase</a> \u{1F852} Table</p><p>Implements <a href="./ITable.html" title="SNow.Core.ITable">ITable</a></p><table><thead><tr><th style="text-align:left;">Constructors</th><th style="text-align:left;"></th></tr></thead><tbody><tr><td style="text-align:left;"><a href="./Table_Table(IServiceNow_string_ILogger).html" title="SNow.Core.Table.Table(SNow.Core.IServiceNow, string, Microsoft.Extensions.Logging.ILogger)">Table(IServiceNow, string, ILogger)</a></td><td style="text-align:left;">Used by typed and untyped Table</td></tr></tbody></table><table><thead><tr><th style="text-align:left;">Methods</th><th style="text-align:left;"></th></tr></thead><tbody><tr><td style="text-align:left;"><a href="./Table_AllToListAsync().html" title="SNow.Core.Table.AllToListAsync()">AllToListAsync()</a></td><td style="text-align:left;">Makes HTTP requests to get all data (from all pages)</td></tr><tr><td style="text-align:left;"><a href="./Table_Create(object).html" title="SNow.Core.Table.Create(object)">Create(object)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./Table_GetByIdAsync(Guid).html" title="SNow.Core.Table.GetByIdAsync(System.Guid)">GetByIdAsync(Guid)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./Table_Limit(int).html" title="SNow.Core.Table.Limit(int)">Limit(int)</a></td><td style="text-align:left;">The maximum number of results returned per page (default: 10,000)</td></tr><tr><td style="text-align:left;"><a href="./Table_OrderBy(string).html" title="SNow.Core.Table.OrderBy(string)">OrderBy(string)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./Table_OrderByDesc(string).html" title="SNow.Core.Table.OrderByDesc(string)">OrderByDesc(string)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./Table_Select(string__).html" title="SNow.Core.Table.Select(string[])">Select(string[])</a></td><td style="text-align:left;">List of properties to return, <br> impacts the size of the response</td></tr><tr><td style="text-align:left;"><a href="./Table_SetHeaders(List_KeyValuePair_string_string__).html" title="SNow.Core.Table.SetHeaders(System.Collections.Generic.List&lt;System.Collections.Generic.KeyValuePair&lt;string,string&gt;&gt;)">SetHeaders(List&lt;KeyValuePair&lt;string,string&gt;&gt;)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./Table_ToListAsync(Nullable_int_).html" title="SNow.Core.Table.ToListAsync(System.Nullable&lt;int&gt;)">ToListAsync(Nullable&lt;int&gt;)</a></td><td style="text-align:left;">Makes the actual HTTP request</td></tr><tr><td style="text-align:left;"><a href="./Table_Update(Guid_object_bool).html" title="SNow.Core.Table.Update(System.Guid, object, bool)">Update(Guid, object, bool)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./Table_WithQuery(string).html" title="SNow.Core.Table.WithQuery(string)">WithQuery(string)</a></td><td style="text-align:left;">The query must have only those operators <br> and, or, like, =, !=, startsWith, endsWith <br> see <a href="https://docs.servicenow.com/bundle/rome-application-development/page/integrate/inbound-rest/concept/c_RESTAPI.html" title="https://docs.servicenow.com/bundle/rome-application-development/page/integrate/inbound-rest/concept/c_RESTAPI.html" target="_blank" rel="noopener noreferrer">SN Rest Operators</a></td></tr></tbody></table>`,8),n=[r];function o(i,d,c,h,b,p){return a(),e("div",null,n)}var f=t(s,[["render",o]]);export{y as __pageData,f as default};
