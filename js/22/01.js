const decks = require("fs")
    .readFileSync(require("path").join(__dirname, "input.txt"), "utf8")
    .split("\r\n\r\n")
    .map((line) =>
        line
            .match(/[0-9]+(?!:)/g)
            .map(Number)
            .reverse()
    );

while (decks.every((deck) => deck.length > 0)) {
    const one = decks[0].pop();
    const two = decks[1].pop();

    if (one > two) {
        decks[0].unshift(one);
        decks[0].unshift(two);
    } else {
        decks[1].unshift(two);
        decks[1].unshift(one);
    }
}

const score = decks
    .filter((deck) => !!deck.length)[0]
    .map((n, i) => n * (i + 1))
    .reduce((a, b) => a + b);

console.log(score);
