const input = require("fs")
    .readFileSync(require("path").join(__dirname, "input.txt"), "utf8")
    .split("\r\n")
    .map(Number);

input.unshift(0);
input.push(Math.max.apply(null, input) + 3);

const map = input
    .sort((a, b) => a - b)
    .map((n, i, a) => a[i + 1] - n || 0)
    .filter((x) => !!x)
    .reduce(
        (entryMap, e) => entryMap.set(e, [...(entryMap.get(e) || []), e]),
        new Map()
    );

const result = [...map].map((e) => e[1].length).reduce((a, b) => a * b);

console.log(result);
