import GetAutoSideBar from "./theme/auto-sidebar";

module.exports = {
  title: "ServiceNow.Core",
  description: "Your easy to use REST API integration library",
  base: "/ServiceNow.Core/",

  themeConfig: {
    repo: "emersonbottero/ServiceNow.Core",
    docsDir: "docs",
    docsBranch: "main",

    algolia: {
      appId: "HK9BCTMQOX",
      apiKey: "8119408cf84cbade754f568a5075a213",
      indexName: "servicenow",
    },

    nav: [
      {
        text: "Guide",
        link: "/guide/getting-started",
        activeMatch: "^/guide/",
      },
      {
        text: "Config Reference",
        link: "/config/Authentication",
        activeMatch: "^/config/",
      },
      { text: "Auto Generated", link: "/auto/", activeMatch: "^/auto/" },
      {
        text: "Release Notes",
        link: "https://github.com/emersonbottero/ServiceNow.Core/releases",
      },
    ],

    sidebar: {
      "/guide/": getGuideSidebar(),
      "/config/": getConfigSidebar(),
      "/auto/": GetAutoSideBar("/auto/"),
    },
  },
};

function getGuideSidebar() {
  return [
    {
      text: "Introduction",
      items: [
        { text: "Home", link: "/" },
        { text: "Getting Started", link: "/guide/getting-started" },
        { text: "Configuration", link: "/config/Authentication" },
      ],
    },
    {
      text: "Advanced",
      items: [
        { text: "Custom Flow", link: "/guide/catalog-item" },
        { text: "Import Set", link: "/guide/import-set" },
      ],
    },
  ];
}

function getConfigSidebar() {
  return [
    {
      text: "Configurations",
      items: [
        { text: "Home", link: "/" },
        { text: "Authentication", link: "/config/Authentication" },
        { text: "Serializers", link: "/config/Serializers" },
      ],
    },
  ];
}
