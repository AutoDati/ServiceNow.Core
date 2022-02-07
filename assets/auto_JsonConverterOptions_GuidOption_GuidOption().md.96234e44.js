import{_ as n,c as o,o as t,d as e}from"./app.2c43e175.js";const h='{"title":"ServiceNow.Core","description":"","frontmatter":{},"headers":[{"level":3,"title":"SNow.Core.Utils.JsonConverterOptions.GuidOption","slug":"snow-core-utils-jsonconverteroptions-guidoption"},{"level":2,"title":"JsonConverterOptions.GuidOption.GuidOption() Constructor","slug":"jsonconverteroptions-guidoption-guidoption-constructor"}],"relativePath":"auto/JsonConverterOptions_GuidOption_GuidOption().md","lastUpdated":1644273326855}',s={},i=e(`<h4 id="servicenow-core" tabindex="-1"><a href="./" title="index">ServiceNow.Core</a> <a class="header-anchor" href="#servicenow-core" aria-hidden="true">#</a></h4><h3 id="snow-core-utils-jsonconverteroptions-guidoption" tabindex="-1"><a href="./SNow_Core_Utils.html" title="SNow.Core.Utils">SNow.Core.Utils</a>.<a href="./JsonConverterOptions.html" title="SNow.Core.Utils.JsonConverterOptions">JsonConverterOptions</a>.<a href="./JsonConverterOptions_GuidOption.html" title="SNow.Core.Utils.JsonConverterOptions.GuidOption">GuidOption</a> <a class="header-anchor" href="#snow-core-utils-jsonconverteroptions-guidoption" aria-hidden="true">#</a></h3><h2 id="jsonconverteroptions-guidoption-guidoption-constructor" tabindex="-1">JsonConverterOptions.GuidOption.GuidOption() Constructor <a class="header-anchor" href="#jsonconverteroptions-guidoption-guidoption-constructor" aria-hidden="true">#</a></h2><p>Handle ServiceNow Guid serialization, if it is an object like</p><div class="language-csharp"><pre><code>
business_service&quot;<span class="token punctuation">:</span> <span class="token punctuation">{</span>
   <span class="token string">&quot;link&quot;</span><span class="token punctuation">:</span> <span class="token string">&quot;https://dev.service-now.com/api/now/table/cmdb_ci_service/ce02b8461b88f01030cb635bbc4bcb6d&quot;</span><span class="token punctuation">,</span>
   <span class="token string">&quot;value&quot;</span><span class="token punctuation">:</span> <span class="token string">&quot;ce02b8461b88f01030cb635bbc4bcb6d&quot;</span>
 <span class="token punctuation">}</span>
</code></pre></div><p>it will read until it reaches &quot;ce02b8461b88f01030cb635bbc4bcb6d&quot; if it is an string it will read the value</p><div class="language-csharp"><pre><code><span class="token keyword">public</span> <span class="token function">GuidOption</span><span class="token punctuation">(</span><span class="token punctuation">)</span><span class="token punctuation">;</span>
</code></pre></div>`,7),a=[i];function r(c,p,u,d,l,_){return t(),o("div",null,a)}var b=n(s,[["render",r]]);export{h as __pageData,b as default};