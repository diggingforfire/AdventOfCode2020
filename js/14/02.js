const input = require("fs")
    .readFileSync(require("path").join(__dirname, "input.txt"), "utf8")
    .split("\r\n");

const sum = run(input, 0);

console.log(sum);

function run(input, index, mask, mem) {
    mem = mem || {};
    const next = input[index];

    if (next) {
        if (next.startsWith("mask")) {
            mask = next.split("=")[1].trimStart();
        } else {
            const address = next.match(/\[(.*?)\]/)[1];
            const value = next.split("=")[1].trimStart();
            applyMask(address, mask).forEach((maskedAddress) => {
                mem[maskedAddress] = parseInt(value);
            });
        }
        return run(input, ++index, mask, mem);
    } else {
        return Object.keys(mem)
            .map((key) => mem[key])
            .filter((x) => !!x)
            .reduce((a, b) => a + b);
    }
}

function applyMask(value, mask) {
    const maskedAddress = (value >>> 0)
        .toString(2)
        .padStart(mask.length, "0")
        .split("")
        .map((bit, index) =>
            mask[index] === "0" ? bit : mask[index] === "1" ? "1" : "X"
        );

    const xCount = maskedAddress.filter((c) => c === "X").length;
    const cartesianProd = cartesian(
        ...Array.from(new Array(xCount), () => [0, 1])
    );

    const maskedAddressString = maskedAddress.join("");

    const res = cartesianProd.map((combination) => {
        let newAdd = maskedAddressString;
        combination.forEach((val) => {
            newAdd = newAdd.replace("X", val);
        });
        return parseInt(newAdd, 2);
    });

    return res;
}

function cartesian(...a) {
    return a.reduce((a, b) => a.flatMap((d) => b.map((e) => [d, e].flat())));
}
