const fs = require("fs");
const path = require("path");

const trees = fs
    .readFileSync(path.join(__dirname, "input.txt"), "utf8")
    .split("\r\n")
	.map((line, y) => line
		.split("")
		.map((char, x) => ({ char, x, y, gridLength: line.length }))
		.filter(point => point.char === "#")
    )
    .reduce((a, b) => a.concat(b))
    .filter(tree => tree.x === (3 * tree.y) % tree.gridLength);

console.log(trees.length);
