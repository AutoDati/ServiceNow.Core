import{_ as t,c as e,o as s,a as n}from"./app.3a5b6c1d.js";const m='{"title":"ServiceNow.Core","description":"","frontmatter":{},"headers":[{"level":3,"title":"SNow.Core.Extensions.HttpClientExtensions","slug":"snow-core-extensions-httpclientextensions"},{"level":2,"title":"HttpClientExtensions.PostActionResultAsync<T>(HttpClient, string, object, Func<Task<string>>) Method","slug":"httpclientextensions-postactionresultasync-t-httpclient-string-object-func-task-string-method"}],"relativePath":"HttpClientExtensions_PostActionResultAsync_T_(HttpClient_string_object_Func_Task_string__).md","lastUpdated":1642564941484}',a={},o=n('<h4 id="servicenow-core" tabindex="-1"><a href="./baseindex.html" title="baseindex">ServiceNow.Core</a> <a class="header-anchor" href="#servicenow-core" aria-hidden="true">#</a></h4><h3 id="snow-core-extensions-httpclientextensions" tabindex="-1"><a href="./SNow_Core_Extensions.html" title="SNow.Core.Extensions">SNow.Core.Extensions</a>.<a href="./HttpClientExtensions.html" title="SNow.Core.Extensions.HttpClientExtensions">HttpClientExtensions</a> <a class="header-anchor" href="#snow-core-extensions-httpclientextensions" aria-hidden="true">#</a></h3><h2 id="httpclientextensions-postactionresultasync-t-httpclient-string-object-func-task-string-method" tabindex="-1">HttpClientExtensions.PostActionResultAsync&lt;T&gt;(HttpClient, string, object, Func&lt;Task&lt;string&gt;&gt;) Method <a class="header-anchor" href="#httpclientextensions-postactionresultasync-t-httpclient-string-object-func-task-string-method" aria-hidden="true">#</a></h2><p>Use to handle POST communication between API&#39;s</p><div class="language-csharp"><pre><code><span class="token keyword">public</span> <span class="token keyword">static</span> <span class="token return-type class-name">System<span class="token punctuation">.</span>Threading<span class="token punctuation">.</span>Tasks<span class="token punctuation">.</span>Task<span class="token punctuation">&lt;</span>T<span class="token punctuation">&gt;</span></span> <span class="token generic-method"><span class="token function">PostActionResultAsync</span><span class="token generic class-name"><span class="token punctuation">&lt;</span>T<span class="token punctuation">&gt;</span></span></span><span class="token punctuation">(</span><span class="token keyword">this</span> <span class="token class-name">System<span class="token punctuation">.</span>Net<span class="token punctuation">.</span>Http<span class="token punctuation">.</span>HttpClient</span> client<span class="token punctuation">,</span> <span class="token class-name"><span class="token keyword">string</span></span> requestUri<span class="token punctuation">,</span> <span class="token class-name"><span class="token keyword">object</span></span> data<span class="token punctuation">,</span> <span class="token class-name">System<span class="token punctuation">.</span>Func<span class="token punctuation">&lt;</span>System<span class="token punctuation">.</span>Threading<span class="token punctuation">.</span>Tasks<span class="token punctuation">.</span>Task<span class="token punctuation">&lt;</span><span class="token keyword">string</span><span class="token punctuation">&gt;</span><span class="token punctuation">&gt;</span></span> authenticate<span class="token punctuation">)</span><span class="token punctuation">;</span>\n</code></pre></div><h4 id="type-parameters" tabindex="-1">Type parameters <a class="header-anchor" href="#type-parameters" aria-hidden="true">#</a></h4><p><a name="SNow_Core_Extensions_HttpClientExtensions_PostActionResultAsync_T_(System_Net_Http_HttpClient_string_object_System_Func_System_Threading_Tasks_Task_string__)_T"></a><code>T</code><br> A class containing props with attributes of type [JsonPropertyName]</p><h4 id="parameters" tabindex="-1">Parameters <a class="header-anchor" href="#parameters" aria-hidden="true">#</a></h4><p><a name="SNow_Core_Extensions_HttpClientExtensions_PostActionResultAsync_T_(System_Net_Http_HttpClient_string_object_System_Func_System_Threading_Tasks_Task_string__)_client"></a><code>client</code> <a href="https://docs.microsoft.com/en-us/dotnet/api/System.Net.Http.HttpClient" title="System.Net.Http.HttpClient" target="_blank" rel="noopener noreferrer">System.Net.Http.HttpClient</a></p><p><a name="SNow_Core_Extensions_HttpClientExtensions_PostActionResultAsync_T_(System_Net_Http_HttpClient_string_object_System_Func_System_Threading_Tasks_Task_string__)_requestUri"></a><code>requestUri</code> <a href="https://docs.microsoft.com/en-us/dotnet/api/System.String" title="System.String" target="_blank" rel="noopener noreferrer">System.String</a><br> Full request uri</p><p><a name="SNow_Core_Extensions_HttpClientExtensions_PostActionResultAsync_T_(System_Net_Http_HttpClient_string_object_System_Func_System_Threading_Tasks_Task_string__)_data"></a><code>data</code> <a href="https://docs.microsoft.com/en-us/dotnet/api/System.Object" title="System.Object" target="_blank" rel="noopener noreferrer">System.Object</a><br> Data to be sent, usually a model</p><p><a name="SNow_Core_Extensions_HttpClientExtensions_PostActionResultAsync_T_(System_Net_Http_HttpClient_string_object_System_Func_System_Threading_Tasks_Task_string__)_authenticate"></a><code>authenticate</code> <a href="https://docs.microsoft.com/en-us/dotnet/api/System.Func-1" title="System.Func`1" target="_blank" rel="noopener noreferrer">System.Func&lt;</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1" title="System.Threading.Tasks.Task`1" target="_blank" rel="noopener noreferrer">System.Threading.Tasks.Task&lt;</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.String" title="System.String" target="_blank" rel="noopener noreferrer">System.String</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1" title="System.Threading.Tasks.Task`1" target="_blank" rel="noopener noreferrer">&gt;</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Func-1" title="System.Func`1" target="_blank" rel="noopener noreferrer">&gt;</a></p><h4 id="returns" tabindex="-1">Returns <a class="header-anchor" href="#returns" aria-hidden="true">#</a></h4><p><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1" title="System.Threading.Tasks.Task`1" target="_blank" rel="noopener noreferrer">System.Threading.Tasks.Task&lt;</a><a href="./HttpClientExtensions_PostActionResultAsync_T_(HttpClient_string_object_Func_Task_string__).html#SNow_Core_Extensions_HttpClientExtensions_PostActionResultAsync_T_(System_Net_Http_HttpClient_string_object_System_Func_System_Threading_Tasks_Task_string__)_T" title="SNow.Core.Extensions.HttpClientExtensions.PostActionResultAsync&lt;T&gt;(System.Net.Http.HttpClient, string, object, System.Func&lt;System.Threading.Tasks.Task&lt;string&gt;&gt;).T">T</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1" title="System.Threading.Tasks.Task`1" target="_blank" rel="noopener noreferrer">&gt;</a><br> An instance of the provided class, can also be a List T</p>',14),i=[o];function r(c,p,_,l,u,d){return s(),e("div",null,i)}var k=t(a,[["render",r]]);export{m as __pageData,k as default};
