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
    .reduce((a, b) => a.concat(b));

const slopes = [
    { x: 1, y: 1 },
    { x: 3, y: 1 },
    { x: 5, y: 1 },
    { x: 7, y: 1 },
    { x: 1, y: 2 },
];

const treeCountMultiplied = 
	slopes.map(slope =>
		trees.filter(tree => 
			tree.x === ((slope.x * tree.y) / slope.y) % tree.gridLength
		).length
	).reduce((a, b) => a * b);

console.log(treeCountMultiplied);
