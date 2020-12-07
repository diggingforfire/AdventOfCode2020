const targetBagName = "shiny gold";
const bags = require("fs")
	.readFileSync(require("path").join(__dirname, "input.txt"), "utf8")
    .split("\r\n")
    .map(line => line.split("contain "))
    .map(splitLine => ({ 
        name: splitLine[0].trim(),
        innerBags: splitLine[1].split(',').map(bag => ({ 
            amount: Number(bag.trim().substring(0, 1)) || 0,
            name: bag.trim().substring(2, bag.trim().length).trim().replace("s.", "").replace(".", "")
        }))
    }));

function countBags(bagName, allBags) {
	const bag = allBags.filter(b => b.name.startsWith(bagName))[0];
	if (!bag) return 0;
	const sum = bag.innerBags.map(innerBag => innerBag.amount).reduce((a, b) => a + b);
	const nestedSum = bag.innerBags.map(innerBag => innerBag.amount * countBags(innerBag.name, bags)).reduce((a, b) => a + b);
	return sum + nestedSum;
}

console.log(countBags(targetBagName, bags));