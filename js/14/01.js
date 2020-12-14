function applyMask(value, mask) {
    const masked = (value >>> 0).toString(2).padStart(mask.length, '0').split('').map((bit, index) => mask[index] === 'X' ? bit : mask[index]);
    return parseInt(masked.join(""), 2);
}

const input = require("fs")
.readFileSync(require("path").join(__dirname, "input.txt"), "utf8")
.split("\r\n");

function run(input, index, mask, mem) {
    mem = mem || {};
    const next = input[index];

    if (next) {
        if (next.startsWith('mask')) {
            mask = next.split("=")[1].trimStart();
        } else {
            const address = next.match(/\[(.*?)\]/)[1];
            const value = next.split("=")[1].trimStart();
            mem[address] = applyMask(value, mask);
        }
        return run(input, ++index, mask, mem);
    } else {
        return Object.keys(mem).map(key => mem[key]).filter(x => !!x).reduce((a, b) => a + b);
    }
}

const sum = run(input, 0);

console.log(sum);