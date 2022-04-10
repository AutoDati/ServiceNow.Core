import{_ as t,c as e,o as l,d as a}from"./app.ac5b6d71.js";const g='{"title":"ServiceNow.Core","description":"","frontmatter":{},"headers":[{"level":3,"title":"SNow.Core","slug":"snow-core"},{"level":2,"title":"ITable Interface","slug":"itable-interface"}],"relativePath":"auto/ITable.md","lastUpdated":1649560581645}',r={},s=a(`<h4 id="servicenow-core" tabindex="-1"><a href="./" title="index">ServiceNow.Core</a> <a class="header-anchor" href="#servicenow-core" aria-hidden="true">#</a></h4><h3 id="snow-core" tabindex="-1"><a href="./SNow_Core.html" title="SNow.Core">SNow.Core</a> <a class="header-anchor" href="#snow-core" aria-hidden="true">#</a></h3><h2 id="itable-interface" tabindex="-1">ITable Interface <a class="header-anchor" href="#itable-interface" aria-hidden="true">#</a></h2><p>Handle ServiceNow tables API</p><div class="language-csharp"><pre><code><span class="token keyword">public</span> <span class="token keyword">interface</span> <span class="token class-name">ITable</span>
</code></pre></div><p>Derived<br> \u21B3 <a href="./Table.html" title="SNow.Core.Table">Table</a></p><table><thead><tr><th style="text-align:left;">Properties</th><th style="text-align:left;"></th></tr></thead><tbody><tr><td style="text-align:left;"><a href="./ITable_RequestUrl.html" title="SNow.Core.ITable.RequestUrl">RequestUrl</a></td><td style="text-align:left;"></td></tr></tbody></table><table><thead><tr><th style="text-align:left;">Methods</th><th style="text-align:left;"></th></tr></thead><tbody><tr><td style="text-align:left;"><a href="./ITable_AllToListAsync().html" title="SNow.Core.ITable.AllToListAsync()">AllToListAsync()</a></td><td style="text-align:left;">Makes HTTP requests to get all data (from all pages)</td></tr><tr><td style="text-align:left;"><a href="./ITable_Create(object).html" title="SNow.Core.ITable.Create(object)">Create(object)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./ITable_Delete(Guid).html" title="SNow.Core.ITable.Delete(System.Guid)">Delete(Guid)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./ITable_GetByIdAsync(Guid).html" title="SNow.Core.ITable.GetByIdAsync(System.Guid)">GetByIdAsync(Guid)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./ITable_Limit(int).html" title="SNow.Core.ITable.Limit(int)">Limit(int)</a></td><td style="text-align:left;">The maximum number of results returned per page (default: 10,000)</td></tr><tr><td style="text-align:left;"><a href="./ITable_OrderBy(string).html" title="SNow.Core.ITable.OrderBy(string)">OrderBy(string)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./ITable_OrderByDesc(string).html" title="SNow.Core.ITable.OrderByDesc(string)">OrderByDesc(string)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./ITable_Select(string__).html" title="SNow.Core.ITable.Select(string[])">Select(string[])</a></td><td style="text-align:left;">List of properties to return, <br> impacts the size of the response</td></tr><tr><td style="text-align:left;"><a href="./ITable_SetHeaders(List_KeyValuePair_string_string__).html" title="SNow.Core.ITable.SetHeaders(System.Collections.Generic.List&lt;System.Collections.Generic.KeyValuePair&lt;string,string&gt;&gt;)">SetHeaders(List&lt;KeyValuePair&lt;string,string&gt;&gt;)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./ITable_ToListAsync(Nullable_int_).html" title="SNow.Core.ITable.ToListAsync(System.Nullable&lt;int&gt;)">ToListAsync(Nullable&lt;int&gt;)</a></td><td style="text-align:left;">Makes the actual HTTP request</td></tr><tr><td style="text-align:left;"><a href="./ITable_Update(Guid_object_bool).html" title="SNow.Core.ITable.Update(System.Guid, object, bool)">Update(Guid, object, bool)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./ITable_WithQuery(string).html" title="SNow.Core.ITable.WithQuery(string)">WithQuery(string)</a></td><td style="text-align:left;">The query must have only those operators <br> and, or, like, =, !=, startsWith, endsWith <br> see <a href="https://docs.servicenow.com/bundle/rome-application-development/page/integrate/inbound-rest/concept/c_RESTAPI.html" title="https://docs.servicenow.com/bundle/rome-application-development/page/integrate/inbound-rest/concept/c_RESTAPI.html" target="_blank" rel="noopener noreferrer">SN Rest Operators</a></td></tr></tbody></table>`,8),i=[s];function n(o,d,c,h,b,f){return l(),e("div",null,i)}var _=t(r,[["render",n]]);export{g as __pageData,_ as default};
