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
    }))
    .filter(innerBag => !innerBag.name.startsWith(targetBagName))

function hasBag(bagName, targetBagName, allBags, cache) {
	cache = cache || {};
	if (cache.hasOwnProperty(bagName+targetBagName)) {
		return cache[bagName+targetBagName];
	}

	const bag = allBags.filter(b => b.name.startsWith(bagName))[0];
	const containsBag = bagName.startsWith(targetBagName) || (bag && bag.innerBags.some(innerBag => hasBag(innerBag.name, targetBagName, allBags, cache)));
	cache[bagName+targetBagName] = containsBag;
	return containsBag;
}

const containingBags = bags.filter(bag => hasBag(bag.name, targetBagName, bags));

console.log(containingBags.length);
