const seatIds = require("fs")
	.readFileSync(require("path").join(__dirname, "input.txt"), "utf8")
	.split("\r\n")
	.map(seat => {
		const row = parseInt(seat.replace(/F/g, "0").replace(/B/g, "1"), 2);
		const column = parseInt(seat.slice(-3).replace(/L/g, "0").replace(/R/g, "1"), 2);
		return row * 8 + column;
	});

	console.log(Math.max.apply(null, seatIds));