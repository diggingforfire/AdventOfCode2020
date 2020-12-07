const targetBagName = "shiny gold";
const bags = require("fs")
	.readFileSync(require("path").join(__dirname, "input.txt"), "utf8")
    .split("\r\n")
    .map(line => line.split("contain "))
    .map(splitLine => ({ 
        name: splitLine[0].trim(),
        innerBags: splitLine[1].split(',').map(bag => ({ 
            amount: Number(bag.trim().substring(0, 1)) || 0,
            name: bag.trim().substring(2, bag.trim().length).trim().replace("s.", "")
        }))
    }))
    .filter(innerBag => !innerBag.name.startsWith(targetBagName))

function hasBag(bagName, targetBagName, allBags) {
    const bag = allBags.filter(b => b.name.startsWith(bagName))[0];
    return bagName.startsWith(targetBagName) || (bag && bag.innerBags.some(innerBag => hasBag(innerBag.name, targetBagName, allBags)));
}

const containingBags = bags.filter(bag => hasBag(bag.name, targetBagName, bags));

console.log(containingBags.length);
