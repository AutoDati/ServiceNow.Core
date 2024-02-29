import{_ as s,c as i,o as a,U as t}from"./chunks/framework.WPXu0_gW.js";const E=JSON.parse('{"title":"","description":"","frontmatter":{},"headers":[],"relativePath":"guide/catalog-item.md","filePath":"guide/catalog-item.md"}'),e={name:"guide/catalog-item.md"},n=t(`<h2 id="you-can-create-catalogitem" tabindex="-1">You can create CatalogItem <a class="header-anchor" href="#you-can-create-catalogitem" aria-label="Permalink to &quot;You can create CatalogItem&quot;">​</a></h2><p>When a custom flow is defined is possible to make requests to it with the UsingCatalog class</p><p>See <a href="https://docs.servicenow.com/bundle/orlando-it-service-management/page/product/service-catalog-management/task/t_DefineACatalogItem.html" target="_blank" rel="noreferrer">Service Now Docs</a> to learn more</p><ul><li>You must first create an application in studio inside ServiceNow and then: <ul><li>Create a catalogItem</li><li>create a flow</li></ul></li></ul><div class="language-csharp vp-adaptive-theme"><button title="Copy Code" class="copy"></button><span class="lang">csharp</span><pre class="shiki shiki-themes github-light github-dark vp-code"><code><span class="line"><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">var</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> requestCatalog</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;"> =</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;"> serviceNow.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">UsingCatalog</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&lt;</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Request</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&gt;(</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">new</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> Guid</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">(</span><span style="--shiki-light:#032F62;--shiki-dark:#9ECBFF;">&quot;catalogItemIdHere&quot;</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">));</span></span>
<span class="line"></span>
<span class="line"><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">var</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> request</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;"> =</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;"> await</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;"> requestCatalog.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Request</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">(</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">new</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">{</span></span>
<span class="line"><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">    varNameHereString </span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">=</span><span style="--shiki-light:#032F62;--shiki-dark:#9ECBFF;"> &quot;string&quot;</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">,</span></span>
<span class="line"><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">    varNameHereNumber </span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">=</span><span style="--shiki-light:#005CC5;--shiki-dark:#79B8FF;"> 10</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">,</span></span>
<span class="line"><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">    varNameHereReference </span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">=</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;"> new</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> Guid</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">(sys_id),</span></span>
<span class="line"><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">}); ;</span></span></code></pre></div>`,5),l=[n];function h(p,k,r,o,d,c){return a(),i("div",null,l)}const u=s(e,[["render",h]]);export{E as __pageData,u as default};