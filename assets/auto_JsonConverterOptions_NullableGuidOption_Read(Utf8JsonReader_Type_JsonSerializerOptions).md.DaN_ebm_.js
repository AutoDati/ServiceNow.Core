import{_ as e,c as s,o as t,U as i}from"./chunks/framework.WPXu0_gW.js";const u=JSON.parse('{"title":"","description":"","frontmatter":{},"headers":[],"relativePath":"auto/JsonConverterOptions_NullableGuidOption_Read(Utf8JsonReader_Type_JsonSerializerOptions).md","filePath":"auto/JsonConverterOptions_NullableGuidOption_Read(Utf8JsonReader_Type_JsonSerializerOptions).md"}'),a={name:"auto/JsonConverterOptions_NullableGuidOption_Read(Utf8JsonReader_Type_JsonSerializerOptions).md"},n=i('<h4 id="servicenow-core" tabindex="-1"><a href="./" title="index">ServiceNow.Core</a> <a class="header-anchor" href="#servicenow-core" aria-label="Permalink to &quot;[ServiceNow.Core](index.md &#39;index&#39;)&quot;">​</a></h4><h3 id="snow-core-utils-jsonconverteroptions-nullableguidoption" tabindex="-1"><a href="./SNow_Core_Utils.html" title="SNow.Core.Utils">SNow.Core.Utils</a>.<a href="./JsonConverterOptions.html" title="SNow.Core.Utils.JsonConverterOptions">JsonConverterOptions</a>.<a href="./JsonConverterOptions_NullableGuidOption.html" title="SNow.Core.Utils.JsonConverterOptions.NullableGuidOption">NullableGuidOption</a> <a class="header-anchor" href="#snow-core-utils-jsonconverteroptions-nullableguidoption" aria-label="Permalink to &quot;[SNow.Core.Utils](SNow_Core_Utils.md &#39;SNow.Core.Utils&#39;).[JsonConverterOptions](JsonConverterOptions.md &#39;SNow.Core.Utils.JsonConverterOptions&#39;).[NullableGuidOption](JsonConverterOptions_NullableGuidOption.md &#39;SNow.Core.Utils.JsonConverterOptions.NullableGuidOption&#39;)&quot;">​</a></h3><h2 id="jsonconverteroptions-nullableguidoption-read-utf8jsonreader-type-jsonserializeroptions-method" tabindex="-1">JsonConverterOptions.NullableGuidOption.Read(Utf8JsonReader, Type, JsonSerializerOptions) Method <a class="header-anchor" href="#jsonconverteroptions-nullableguidoption-read-utf8jsonreader-type-jsonserializeroptions-method" aria-label="Permalink to &quot;JsonConverterOptions.NullableGuidOption.Read(Utf8JsonReader, Type, JsonSerializerOptions) Method&quot;">​</a></h2><div class="language-csharp vp-adaptive-theme"><button title="Copy Code" class="copy"></button><span class="lang">csharp</span><pre class="shiki shiki-themes github-light github-dark vp-code"><code><span class="line"><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">public</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;"> override</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> System</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Nullable</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&lt;</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">System</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Guid</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">&gt; </span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Read</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">(</span><span style="--shiki-light:#D73A49;--shiki-dark:#F97583;">ref</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> System</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Text</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Json</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Utf8JsonReader</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> reader</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">, </span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">System</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Type</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> typeToConvert</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">, </span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">System</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Text</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">Json</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">.</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;">JsonSerializerOptions</span><span style="--shiki-light:#6F42C1;--shiki-dark:#B392F0;"> options</span><span style="--shiki-light:#24292E;--shiki-dark:#E1E4E8;">);</span></span></code></pre></div><h4 id="parameters" tabindex="-1">Parameters <a class="header-anchor" href="#parameters" aria-label="Permalink to &quot;Parameters&quot;">​</a></h4><p><a name="SNow_Core_Utils_JsonConverterOptions_NullableGuidOption_Read(System_Text_Json_Utf8JsonReader_System_Type_System_Text_Json_JsonSerializerOptions)_reader"></a><code>reader</code> <a href="https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.Utf8JsonReader" title="System.Text.Json.Utf8JsonReader" target="_blank" rel="noreferrer">System.Text.Json.Utf8JsonReader</a></p><p><a name="SNow_Core_Utils_JsonConverterOptions_NullableGuidOption_Read(System_Text_Json_Utf8JsonReader_System_Type_System_Text_Json_JsonSerializerOptions)_typeToConvert"></a><code>typeToConvert</code> <a href="https://docs.microsoft.com/en-us/dotnet/api/System.Type" title="System.Type" target="_blank" rel="noreferrer">System.Type</a></p><p><a name="SNow_Core_Utils_JsonConverterOptions_NullableGuidOption_Read(System_Text_Json_Utf8JsonReader_System_Type_System_Text_Json_JsonSerializerOptions)_options"></a><code>options</code> <a href="https://docs.microsoft.com/en-us/dotnet/api/System.Text.Json.JsonSerializerOptions" title="System.Text.Json.JsonSerializerOptions" target="_blank" rel="noreferrer">System.Text.Json.JsonSerializerOptions</a></p><h4 id="returns" tabindex="-1">Returns <a class="header-anchor" href="#returns" aria-label="Permalink to &quot;Returns&quot;">​</a></h4><p><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1" title="System.Nullable`1" target="_blank" rel="noreferrer">System.Nullable&lt;</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Guid" title="System.Guid" target="_blank" rel="noreferrer">System.Guid</a><a href="https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1" title="System.Nullable`1" target="_blank" rel="noreferrer">&gt;</a></p>',10),o=[n];function r(l,p,h,d,k,_){return t(),s("div",null,o)}const c=e(a,[["render",r]]);export{u as __pageData,c as default};
