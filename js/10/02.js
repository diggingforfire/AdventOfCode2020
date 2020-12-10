const input = require("fs")
    .readFileSync(require("path").join(__dirname, "input.txt"), "utf8")
    .split("\r\n")
    .map(Number);

input.unshift(0);
input.push(Math.max.apply(null, input) + 3);

const graph = input
    .sort((a, b) => a - b)
    .map((n, index, all) => {
        return {
            n: n,
            nextInRange: all
                .slice(index + 1, index + 4)
                .filter((x) => x - n <= 3),
        };
    });

function depth(node, cache) {
    cache = cache || {};
    if (cache.hasOwnProperty(node.n)) {
        return cache[node.n];
    }

    if (!node.nextInRange.length) {
        return 1;
    } else {
        const nodeDepth = node.nextInRange
            .map((inner) => {
                const innerNode = graph.filter((g) => g.n === inner)[0];
                return depth(innerNode, cache);
            })
            .reduce((a, b) => a + b);
        cache[node.n] = nodeDepth;
        return nodeDepth;
    }
}

const distinctNumberOfConnections = depth(graph[0]);

console.log(distinctNumberOfConnections);
