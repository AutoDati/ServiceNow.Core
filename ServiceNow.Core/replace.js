const replaceInFiles = require("replace-in-files");



const options = {

    files: "Docs/**/*.md",

    from: /\[.*?\]/gi, // string or regex

    to: function(match) { 

        return match.replace(/<\/?[^>]*?>/gi,' ');
    
    }

};



async function main() {

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