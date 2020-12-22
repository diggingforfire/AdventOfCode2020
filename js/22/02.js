const initialDecks = require("fs")
    .readFileSync(require("path").join(__dirname, "input.txt"), "utf8")
    .split("\r\n\r\n")
    .map((line) =>
        line
            .match(/[0-9]+(?!:)/g)
            .map(Number)
            .reverse()
    );

function play(decks) {
    const previousDecks = [];

    while (decks.every((deck) => deck.length > 0)) {
        const deckOne = JSON.stringify(decks[0]);
        const deckTwo = JSON.stringify(decks[1]);

        const decksWereSeenBefore = previousDecks.some((previousDeck) => {
            return previousDeck[0] === deckOne || previousDeck[1] === deckTwo;
        });

        if (decksWereSeenBefore) {
            return true; // true if p1 wins
        } else {
            const copy = decks.map((copy) => JSON.stringify(copy)); // lean and mean
            previousDecks.push(copy);

            const one = decks[0].pop();
            const two = decks[1].pop();
            let p1Wins = one > two;

            if (decks[0].length >= one && decks[1].length >= two) {
                const newDeckOne = decks[0].slice(
                    decks[0].length - one,
                    one + 1
                );
                const newDeckTwo = decks[1].slice(
                    decks[1].length - two,
                    two + 1
                );

                p1Wins = play([newDeckOne, newDeckTwo]);
            }

            if (p1Wins) {
                decks[0].unshift(one);
                decks[0].unshift(two);
            } else {
                decks[1].unshift(two);
                decks[1].unshift(one);
            }
        }
    }

    return !!decks[0].length; // true if p1 wins
}

play(initialDecks);

const score = initialDecks
    .filter((deck) => !!deck.length)[0]
    .map((n, i) => n * (i + 1))
    .reduce((a, b) => a + b);

console.log(score);
