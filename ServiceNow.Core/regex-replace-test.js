var matches = "This is <span>Outside content </span> and inside is [<T>Description of project (only for)]".replace(/\[.*?\]/gi,function(match) { 

    return match.replace(/<\/?[^>]*?>/gi,' ');

});

console.log(matches)