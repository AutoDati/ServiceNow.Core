import{_ as t,c as e,o,a as r}from"./app.3a5b6c1d.js";const N='{"title":"ServiceNow.Core","description":"","frontmatter":{},"headers":[{"level":3,"title":"SNow.Core","slug":"snow-core"},{"level":2,"title":"ServiceNow Class","slug":"servicenow-class"}],"relativePath":"ServiceNow.md","lastUpdated":1642564941484}',a={},i=r(`<h4 id="servicenow-core" tabindex="-1"><a href="./baseindex.html" title="baseindex">ServiceNow.Core</a> <a class="header-anchor" href="#servicenow-core" aria-hidden="true">#</a></h4><h3 id="snow-core" tabindex="-1"><a href="./SNow_Core.html" title="SNow.Core">SNow.Core</a> <a class="header-anchor" href="#snow-core" aria-hidden="true">#</a></h3><h2 id="servicenow-class" tabindex="-1">ServiceNow Class <a class="header-anchor" href="#servicenow-class" aria-hidden="true">#</a></h2><div class="language-csharp"><pre><code><span class="token keyword">public</span> <span class="token keyword">class</span> <span class="token class-name">ServiceNow</span> <span class="token punctuation">:</span>
<span class="token type-list"><span class="token class-name">SNow<span class="token punctuation">.</span>Core<span class="token punctuation">.</span>IServiceNow</span></span>
</code></pre></div><p>Inheritance <a href="https://docs.microsoft.com/en-us/dotnet/api/System.Object" title="System.Object" target="_blank" rel="noopener noreferrer">System.Object</a> \u{1F852} ServiceNow</p><p>Implements <a href="./IServiceNow.html" title="SNow.Core.IServiceNow">IServiceNow</a></p><table><thead><tr><th style="text-align:left;">Constructors</th><th style="text-align:left;"></th></tr></thead><tbody><tr><td style="text-align:left;"><a href="./ServiceNow_ServiceNow(IConfiguration_JsonConverter__).html" title="SNow.Core.ServiceNow.ServiceNow(Microsoft.Extensions.Configuration.IConfiguration, System.Text.Json.Serialization.JsonConverter[])">ServiceNow(IConfiguration, JsonConverter[])</a></td><td style="text-align:left;">Your configuration must have a session named AzureAd or simples authentication data (UserName , Password and BaseAddress)</td></tr><tr><td style="text-align:left;"><a href="./ServiceNow_ServiceNow(AuthenticationConfig_JsonConverter__).html" title="SNow.Core.ServiceNow.ServiceNow(SNow.Core.Authentication.AuthenticationConfig, System.Text.Json.Serialization.JsonConverter[])">ServiceNow(AuthenticationConfig, JsonConverter[])</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./ServiceNow_ServiceNow(BasicAuthenticationConfig_JsonConverter__).html" title="SNow.Core.ServiceNow.ServiceNow(SNow.Core.Authentication.BasicAuthenticationConfig, System.Text.Json.Serialization.JsonConverter[])">ServiceNow(BasicAuthenticationConfig, JsonConverter[])</a></td><td style="text-align:left;"></td></tr></tbody></table><table><thead><tr><th style="text-align:left;">Properties</th><th style="text-align:left;"></th></tr></thead><tbody><tr><td style="text-align:left;"><a href="./ServiceNow_BasicAuthParams.html" title="SNow.Core.ServiceNow.BasicAuthParams">BasicAuthParams</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./ServiceNow_Token.html" title="SNow.Core.ServiceNow.Token">Token</a></td><td style="text-align:left;"></td></tr></tbody></table><table><thead><tr><th style="text-align:left;">Methods</th><th style="text-align:left;"></th></tr></thead><tbody><tr><td style="text-align:left;"><a href="./ServiceNow_UsingCatalog(Guid).html" title="SNow.Core.ServiceNow.UsingCatalog(System.Guid)">UsingCatalog(Guid)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./ServiceNow_UsingCatalog_T_(Guid).html" title="SNow.Core.ServiceNow.UsingCatalog&lt;T&gt;(System.Guid)">UsingCatalog&lt;T&gt;(Guid)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./ServiceNow_UsingImportSet(string).html" title="SNow.Core.ServiceNow.UsingImportSet(string)">UsingImportSet(string)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./ServiceNow_UsingTable(string_ILogger).html" title="SNow.Core.ServiceNow.UsingTable(string, Microsoft.Extensions.Logging.ILogger)">UsingTable(string, ILogger)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./ServiceNow_UsingTable_T_(ILogger).html" title="SNow.Core.ServiceNow.UsingTable&lt;T&gt;(Microsoft.Extensions.Logging.ILogger)">UsingTable&lt;T&gt;(ILogger)</a></td><td style="text-align:left;"></td></tr><tr><td style="text-align:left;"><a href="./ServiceNow_UsingTable_T_(string_ILogger).html" title="SNow.Core.ServiceNow.UsingTable&lt;T&gt;(string, Microsoft.Extensions.Logging.ILogger)">UsingTable&lt;T&gt;(string, ILogger)</a></td><td style="text-align:left;"></td></tr></tbody></table><table><thead><tr><th style="text-align:left;">Explicit Interface Implementations</th><th style="text-align:left;"></th></tr></thead><tbody><tr><td style="text-align:left;"><a href="./ServiceNow_SNow_Core_IServiceNow_AuthenticateAsync().html" title="SNow.Core.ServiceNow.SNow.Core.IServiceNow.AuthenticateAsync()">SNow.Core.IServiceNow.AuthenticateAsync()</a></td><td style="text-align:left;">Updates the ServiceNow Token internal property</td></tr><tr><td style="text-align:left;"><a href="./ServiceNow_SNow_Core_IServiceNow_BaseAddress.html" title="SNow.Core.ServiceNow.SNow.Core.IServiceNow.BaseAddress">SNow.Core.IServiceNow.BaseAddress</a></td><td style="text-align:left;"></td></tr></tbody></table>`,10),n=[i];function s(l,c,d,g,h,S){return o(),e("div",null,n)}var f=t(a,[["render",s]]);export{N as __pageData,f as default};
