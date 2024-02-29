import{_ as s,c as i,o as a,U as e}from"./chunks/framework.WPXu0_gW.js";const c=JSON.parse('{"title":"","description":"","frontmatter":{},"headers":[],"relativePath":"guide/import-set.md","filePath":"guide/import-set.md"}'),t={name:"guide/import-set.md"},n=e(`<h2 id="you-can-use-importset-s" tabindex="-1">You can use ImportSet&#39;s <a class="header-anchor" href="#you-can-use-importset-s" aria-label="Permalink to &quot;You can use ImportSet&#39;s&quot;">​</a></h2><p>Used to send data to serviceNow like an feed process. Allows multiples import set be exported in a single request.</p><p>See <a href="https://docs.servicenow.com/bundle/orlando-it-service-management/page/product/service-catalog-management/task/t_DefineACatalogItem.html" target="_blank" rel="noreferrer">Service Now Docs</a> to learn more</p><ul><li>You must first create an application in studio inside ServiceNow and then: <ul><li>Create a catalogItem</li><li>create a flow</li></ul></li></ul><div class="language-csharp vp-adaptive-theme"><button title="Copy Code" class="copy"></button><span class="lang">csharp</span><pre class="shiki shiki-themes github-light github-dark vp-code"><code><span class="line"><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">var</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> requestCatalog</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;"> =</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;"> serviceNow.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">UsingCatalog</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&lt;</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Request</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&gt;(</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">new</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> Guid</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">(</span><span style="--shiki-light:#032F62;--shiki-dark:#9ECBFF;">&quot;catalogItemIdHere&quot;</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">));</span></span>
<span class="line"></span>
<span class="line"><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">var</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> request</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;"> =</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;"> await</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;"> requestCatalog.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Request</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">(</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">new</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">{</span></span>
<span class="line"><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">    varNameHereString </span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">=</span><span style="--shiki-light:#032F62;--shiki-dark:#9ECBFF;"> &quot;string&quot;</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">,</span></span>
<span class="line"><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">    varNameHereNumber </span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">=</span><span style="--shiki-light:#005CC5;--shiki-dark:#79B8FF;"> 10</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">,</span></span>
<span class="line"><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">    varNameHereReference </span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">=</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;"> new</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> Guid</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">(sys_id),</span></span>
<span class="line"><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">}); ;</span></span></code></pre></div>`,5),l=[n];function h(p,k,r,o,d,E){return a(),i("div",null,l)}const u=s(t,[["render",h]]);export{c as __pageData,u as default};