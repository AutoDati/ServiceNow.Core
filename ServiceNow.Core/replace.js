const replaceInFiles = require("replace-in-files");
const fs = require("fs");
const path = require("path");

const options = {
  files: "docs/applications/**/*.md",
  from: /\[.*?\]/gi, // string or regex

  to: function (match) {
    if (match.includes("<"))
      return match.replace("<", "\\<").replace(">", "\\>");
    return match;
  },
};

async function main() {
  console.warn("Modifying files!!!");

  fs.readdir("./docs", (err, files) => {
    if (err) {
      console.log(err);
    }

    files.forEach((file) => {
      const fileDir = path.join("./docs", file);

      if (file.includes(".md") && file !== "README.md" && file !== "index.md") {
        fs.unlinkSync(fileDir);
      }
    });
  });

  try {
    const { changedFiles, countOfMatchesByPaths, replaceInFilesOptions } =
      await replaceInFiles(options);

    console.log("Modified files:", changedFiles);

    console.log("Count of matches by paths:", countOfMatchesByPaths);

    console.log("was called with:", replaceInFilesOptions);
  } catch (error) {
    console.log("Error occurred:", error);
  }
}

main();
