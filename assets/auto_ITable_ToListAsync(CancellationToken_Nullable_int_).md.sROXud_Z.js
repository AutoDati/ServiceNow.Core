import{_ as e,c as s,o as a,U as t}from"./chunks/framework.WPXu0_gW.js";const m=JSON.parse('{"title":"","description":"","frontmatter":{},"headers":[],"relativePath":"auto/ITable_ToListAsync(CancellationToken_Nullable_int_).md","filePath":"auto/ITable_ToListAsync(CancellationToken_Nullable_int_).md"}'),i={name:"auto/ITable_ToListAsync(CancellationToken_Nullable_int_).md"},n=t('<h4 id="servicenow-core" tabindex="-1"><a href="./" title="index">ServiceNow.Core</a> <a class="header-anchor" href="#servicenow-core" aria-label="Permalink to &quot;[ServiceNow.Core](index.md &#39;index&#39;)&quot;">​</a></h4><h3 id="snow-core-itable" tabindex="-1"><a href="./SNow_Core.html" title="SNow.Core">SNow.Core</a>.<a href="./ITable.html" title="SNow.Core.ITable">ITable</a> <a class="header-anchor" href="#snow-core-itable" aria-label="Permalink to &quot;[SNow.Core](SNow_Core.md &#39;SNow.Core&#39;).[ITable](ITable.md &#39;SNow.Core.ITable&#39;)&quot;">​</a></h3><h2 id="itable-tolistasync-cancellationtoken-nullable-int-method" tabindex="-1">ITable.ToListAsync(CancellationToken, Nullable&lt;int&gt;) Method <a class="header-anchor" href="#itable-tolistasync-cancellationtoken-nullable-int-method" aria-label="Permalink to &quot;ITable.ToListAsync(CancellationToken, Nullable&amp;lt;int&amp;gt;) Method&quot;">​</a></h2><p>Makes the actual HTTP request</p><div class="language-csharp vp-adaptive-theme"><button title="Copy Code" class="copy"></button><span class="lang">csharp</span><pre class="shiki shiki-themes github-light github-dark vp-code"><code><span class="line"><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">System</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Threading</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Tasks</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Task</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&lt;</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">System</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Collections</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Generic</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">List</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&lt;</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">System</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Text</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Json</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">JsonElement</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&gt;&gt; </span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">ToListAsync</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">(</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">System</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Threading</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">CancellationToken</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> cancellationToken</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">, </span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">System</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Nullable</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&lt;</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">int</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&gt; </span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">pageNumber</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">=</span><span style="--shiki-light:#005CC5;--shiki-dark:#79B8FF;">null</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">);</span></span></code></pre></div><h4 id="parameters" tabindex="-1">Parameters <a class="header-anchor" href="#parameters" aria-label="Permalink to &quot;Parameters&quot;">​</a></h4><p><a name="SNow_Core_ITable_ToListAsync(System_Threading_CancellationToken_System_Nullable_int_)_cancellationToken"></a><code>cancellationToken</code> <a href="https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken" title="System.Threading.CancellationToken" target="_blank" rel="noreferrer">System.Threading.CancellationToken</a><br> A cancellation token that can be used by other objects or threads to receive</p><p><a name="SNow_Core_ITable_ToListAsync(System_Threading_CancellationToken_System_Nullable_int_)_pageNumber"></a><code>pageNumber</code> <a href="https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1" title="System.Nullable`1" target="_blank" rel="noreferrer">System.Nullable&lt;</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Int32" title="System.Int32" target="_blank" rel="noreferrer">System.Int32</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1" title="System.Nullable`1" target="_blank" rel="noreferrer">&gt;</a></p><h4 id="returns" tabindex="-1">Returns <a class="header-anchor" href="#returns" aria-label="Permalink to &quot;Returns&quot;">​</a></h4><p><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1" title="System.Threading.Tasks.Task`1" target="_blank" rel="noreferrer">System.Threading.Tasks.Task&lt;</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1" title="System.Collections.Generic.List`1" target="_blank" rel="noreferrer">System.Collections.Generic.List&lt;</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonElement" title="System.Text.Json.JsonElement" target="_blank" rel="noreferrer">System.Text.Json.JsonElement</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1" title="System.Collections.Generic.List`1" target="_blank" rel="noreferrer">&gt;</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1" title="System.Threading.Tasks.Task`1" target="_blank" rel="noreferrer">&gt;</a><br> An generic json element of type Array</p>',10),l=[n];function r(o,h,k,p,c,d){return a(),s("div",null,l)}const E=e(i,[["render",r]]);export{m as __pageData,E as default};