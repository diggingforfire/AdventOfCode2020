const input = require("fs")
.readFileSync(require("path").join(__dirname, "input.txt"), "utf8")
.split("\r\n");

const sum = run(input, 0);

console.log(sum);

function run(input, index, mask, mem) {
    mem = mem || {};
    const next = input[index];

    if (next) {
        if (next.startsWith('mask')) {
            mask = next.split("=")[1].trimStart();
        } else {
            const address = next.match(/\[(.*?)\]/)[1];
            const value = next.split("=")[1].trimStart();
            applyMask(address, mask).forEach(maskedAddress => {
                mem[maskedAddress] = value;
            });
        }
        return run(input, ++index, mask, mem);
    } else {
        return Object.keys(mem).map(key => mem[key]).filter(x => !!x).reduce((a, b) => a + b);
    }
}

function applyMask(value, mask) {
    const masked = 
        (value >>> 0)
        .toString(2)
        .padStart(mask.length, '0')
        .split('')
        .map((bit, index) => 
            mask[index] === '0' ? bit : 
            mask[index] === '1' ? '1' :
            'X');
        
    return parseInt(masked.join(""), 2);
}