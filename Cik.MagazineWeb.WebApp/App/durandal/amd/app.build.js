{
  "name": "durandal/amd/almond-custom",
  "inlineText": true,
  "stubModules": [
    "durandal/amd/text"
  ],
  "paths": {
    "text": "durandal/amd/text"
  },
  "baseUrl": "E:\\OSS\\magazine-website-lite\\MagazineWebSolution\\Cik.MagazineWeb.WebApp\\App",
  "mainConfigFile": "E:\\OSS\\magazine-website-lite\\MagazineWebSolution\\Cik.MagazineWeb.WebApp\\App\\main.js",
  "include": [
    "main",
    "durandal/app",
    "durandal/composition",
    "durandal/events",
    "durandal/http",
    "text!durandal/messageBox.html",
    "durandal/messageBox",
    "durandal/modalDialog",
    "durandal/system",
    "durandal/viewEngine",
    "durandal/viewLocator",
    "durandal/viewModel",
    "durandal/viewModelBinder",
    "durandal/widget",
    "durandal/plugins/router",
    "durandal/transitions/entrance",
    "services/logger",
    "viewmodels/details",
    "viewmodels/home",
    "viewmodels/shell",
    "text!views/details.html",
    "text!views/footer.html",
    "text!views/home.html",
    "text!views/nav.html",
    "text!views/shell.html"
  ],
  "exclude": [],
  "keepBuildDir": true,
  "optimize": "uglify2",
  "out": "E:\\OSS\\magazine-website-lite\\MagazineWebSolution\\Cik.MagazineWeb.WebApp\\App\\main-built.js",
  "pragmas": {
    "build": true
  },
  "wrap": true,
  "insertRequire": [
    "main"
  ]
}