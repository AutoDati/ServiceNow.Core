const fs = require("fs");
const path = require("path");
var md = require("markdown-it")();
const cheerio = require("cheerio");

const dirPath = path.join(__dirname, "../../auto/index.md");
let autoIndex = fs.readFileSync(dirPath).toString();

function GetAutoSideBar(activeMatch) {
  var html = md.render(autoIndex);
  if (autoIndex) {
    const $ = cheerio.load(html);

    const linkObjects = $("a");
    const links = [];
    linkObjects.each((index, element) => {
      links.push({
        text: $(element).text(), // get the text
        link: activeMatch + $(element).attr("href").replace(".md", ""), // get the href attribute
      });
    });

    var urls = [];

    for (var i = 0; i < links.length; i++) {
      urls.push({
        text: links[i].text,
        link: links[i].link,
      });
    }
    return [
      {
        text: activeMatch.replaceAll("/", ""),
        items: urls,
      },
    ];
  }
}

export default GetAutoSideBar;
