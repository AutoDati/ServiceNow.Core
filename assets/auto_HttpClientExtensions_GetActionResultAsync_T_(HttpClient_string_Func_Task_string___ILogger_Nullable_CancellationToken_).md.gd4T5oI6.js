import{_ as t,c as s,o as e,U as i}from"./chunks/framework.WPXu0_gW.js";const d=JSON.parse('{"title":"","description":"","frontmatter":{},"headers":[],"relativePath":"auto/HttpClientExtensions_GetActionResultAsync_T_(HttpClient_string_Func_Task_string___ILogger_Nullable_CancellationToken_).md","filePath":"auto/HttpClientExtensions_GetActionResultAsync_T_(HttpClient_string_Func_Task_string___ILogger_Nullable_CancellationToken_).md"}'),n={name:"auto/HttpClientExtensions_GetActionResultAsync_T_(HttpClient_string_Func_Task_string___ILogger_Nullable_CancellationToken_).md"},a=i('<h4 id="servicenow-core" tabindex="-1"><a href="./" title="index">ServiceNow.Core</a> <a class="header-anchor" href="#servicenow-core" aria-label="Permalink to &quot;[ServiceNow.Core](index.md &#39;index&#39;)&quot;">​</a></h4><h3 id="snow-core-extensions-httpclientextensions" tabindex="-1"><a href="./SNow_Core_Extensions.html" title="SNow.Core.Extensions">SNow.Core.Extensions</a>.<a href="./HttpClientExtensions.html" title="SNow.Core.Extensions.HttpClientExtensions">HttpClientExtensions</a> <a class="header-anchor" href="#snow-core-extensions-httpclientextensions" aria-label="Permalink to &quot;[SNow.Core.Extensions](SNow_Core_Extensions.md &#39;SNow.Core.Extensions&#39;).[HttpClientExtensions](HttpClientExtensions.md &#39;SNow.Core.Extensions.HttpClientExtensions&#39;)&quot;">​</a></h3><h2 id="httpclientextensions-getactionresultasync-t-httpclient-string-func-task-string-ilogger-nullable-cancellationtoken-method" tabindex="-1">HttpClientExtensions.GetActionResultAsync&lt;T&gt;(HttpClient, string, Func&lt;Task&lt;string&gt;&gt;, ILogger, Nullable&lt;CancellationToken&gt;) Method <a class="header-anchor" href="#httpclientextensions-getactionresultasync-t-httpclient-string-func-task-string-ilogger-nullable-cancellationtoken-method" aria-label="Permalink to &quot;HttpClientExtensions.GetActionResultAsync&amp;lt;T&amp;gt;(HttpClient, string, Func&amp;lt;Task&amp;lt;string&amp;gt;&amp;gt;, ILogger, Nullable&amp;lt;CancellationToken&amp;gt;) Method&quot;">​</a></h2><div class="language-csharp vp-adaptive-theme"><button title="Copy Code" class="copy"></button><span class="lang">csharp</span><pre class="shiki shiki-themes github-light github-dark vp-code"><code><span class="line"><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">public</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;"> static</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> System</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Threading</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Tasks</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Task</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&lt;</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">T</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&gt; </span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">GetActionResultAsync</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&lt;</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">T</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&gt;(</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">this</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> HttpClient</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> client</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">, </span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">string</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> requestUri</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">, </span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">System</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Func</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&lt;</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">System</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Threading</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Tasks</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Task</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&lt;</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">string</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&gt;&gt; </span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">authenticate</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">=</span><span style="--shiki-light:#005CC5;--shiki-dark:#79B8FF;">null</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">, </span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Microsoft</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Extensions</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Logging</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">ILogger</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> logger</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">=</span><span style="--shiki-light:#005CC5;--shiki-dark:#79B8FF;">null</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">, </span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">System</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Nullable</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&lt;</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">System</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Threading</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">CancellationToken</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&gt; </span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">cancellationToken</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">=</span><span style="--shiki-light:#005CC5;--shiki-dark:#79B8FF;">null</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">);</span></span></code></pre></div><h4 id="type-parameters" tabindex="-1">Type parameters <a class="header-anchor" href="#type-parameters" aria-label="Permalink to &quot;Type parameters&quot;">​</a></h4><p><a name="SNow_Core_Extensions_HttpClientExtensions_GetActionResultAsync_T_(HttpClient_string_System_Func_System_Threading_Tasks_Task_string___Microsoft_Extensions_Logging_ILogger_System_Nullable_System_Threading_CancellationToken_)_T"></a><code>T</code></p><h4 id="parameters" tabindex="-1">Parameters <a class="header-anchor" href="#parameters" aria-label="Permalink to &quot;Parameters&quot;">​</a></h4><p><a name="SNow_Core_Extensions_HttpClientExtensions_GetActionResultAsync_T_(HttpClient_string_System_Func_System_Threading_Tasks_Task_string___Microsoft_Extensions_Logging_ILogger_System_Nullable_System_Threading_CancellationToken_)_client"></a><code>client</code> <a href="https://docs.microsoft.com/en-us/dotnet/api/System.Net.Http.HttpClient" title="System.Net.Http.HttpClient" target="_blank" rel="noreferrer">System.Net.Http.HttpClient</a></p><p><a name="SNow_Core_Extensions_HttpClientExtensions_GetActionResultAsync_T_(HttpClient_string_System_Func_System_Threading_Tasks_Task_string___Microsoft_Extensions_Logging_ILogger_System_Nullable_System_Threading_CancellationToken_)_requestUri"></a><code>requestUri</code> <a href="https://docs.microsoft.com/en-us/dotnet/api/System.String" title="System.String" target="_blank" rel="noreferrer">System.String</a></p><p><a name="SNow_Core_Extensions_HttpClientExtensions_GetActionResultAsync_T_(HttpClient_string_System_Func_System_Threading_Tasks_Task_string___Microsoft_Extensions_Logging_ILogger_System_Nullable_System_Threading_CancellationToken_)_authenticate"></a><code>authenticate</code> <a href="https://docs.microsoft.com/en-us/dotnet/api/System.Func-1" title="System.Func`1" target="_blank" rel="noreferrer">System.Func&lt;</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1" title="System.Threading.Tasks.Task`1" target="_blank" rel="noreferrer">System.Threading.Tasks.Task&lt;</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.String" title="System.String" target="_blank" rel="noreferrer">System.String</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1" title="System.Threading.Tasks.Task`1" target="_blank" rel="noreferrer">&gt;</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Func-1" title="System.Func`1" target="_blank" rel="noreferrer">&gt;</a></p><p><a name="SNow_Core_Extensions_HttpClientExtensions_GetActionResultAsync_T_(HttpClient_string_System_Func_System_Threading_Tasks_Task_string___Microsoft_Extensions_Logging_ILogger_System_Nullable_System_Threading_CancellationToken_)_logger"></a><code>logger</code> <a href="https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.Logging.ILogger" title="Microsoft.Extensions.Logging.ILogger" target="_blank" rel="noreferrer">Microsoft.Extensions.Logging.ILogger</a></p><p><a name="SNow_Core_Extensions_HttpClientExtensions_GetActionResultAsync_T_(HttpClient_string_System_Func_System_Threading_Tasks_Task_string___Microsoft_Extensions_Logging_ILogger_System_Nullable_System_Threading_CancellationToken_)_cancellationToken"></a><code>cancellationToken</code> <a href="https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1" title="System.Nullable`1" target="_blank" rel="noreferrer">System.Nullable&lt;</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken" title="System.Threading.CancellationToken" target="_blank" rel="noreferrer">System.Threading.CancellationToken</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1" title="System.Nullable`1" target="_blank" rel="noreferrer">&gt;</a></p><h4 id="returns" tabindex="-1">Returns <a class="header-anchor" href="#returns" aria-label="Permalink to &quot;Returns&quot;">​</a></h4><p><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1" title="System.Threading.Tasks.Task`1" target="_blank" rel="noreferrer">System.Threading.Tasks.Task&lt;</a><a href="./HttpClientExtensions_GetActionResultAsync_T_(HttpClient_string_Func_Task_string___ILogger_Nullable_CancellationToken_).html#SNow_Core_Extensions_HttpClientExtensions_GetActionResultAsync_T_(HttpClient_string_System_Func_System_Threading_Tasks_Task_string___Microsoft_Extensions_Logging_ILogger_System_Nullable_System_Threading_CancellationToken_)_T" title="SNow.Core.Extensions.HttpClientExtensions.GetActionResultAsync&lt;T&gt;(HttpClient, string, System.Func&lt;System.Threading.Tasks.Task&lt;string&gt;&gt;, Microsoft.Extensions.Logging.ILogger, System.Nullable&lt;System.Threading.CancellationToken&gt;).T">T</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1" title="System.Threading.Tasks.Task`1" target="_blank" rel="noreferrer">&gt;</a></p>',14),l=[a];function r(o,h,_,k,g,p){return e(),s("div",null,l)}const y=t(n,[["render",r]]);export{d as __pageData,y as default};
