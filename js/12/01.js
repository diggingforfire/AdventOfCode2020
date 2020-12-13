const actions = require("fs")
    .readFileSync(require("path").join(__dirname, "input.txt"), "utf8")
    .split("\r\n")
    .map(line => { return {
            action: line[0], 
            value: Number(line.slice(1, line.length)) 
        };});

const directions = ['N', 'E', 'S', 'W'];

function getNewPosition(direction, currentPosition, value) {
    switch (direction) {
        case 'N':
            currentPosition.y -= value;
            break;
        case 'E':
            currentPosition.x += value;
            break;
        case 'S':
            currentPosition.y += value;
            break;
        case 'W':
            currentPosition.x -= value;
            break;
    }

    return currentPosition;
}

function getNewDirection(currentDirection, value) {
    let next = (directions.indexOf(currentDirection) + (value / 90)) % directions.length;
    if (next < 0) {
        next = directions.length + next;
    }
    return directions[next];
}

function move(actions, index, currentPosition, currentDirection) {
    const action = actions[index];
    if (action) {
        switch (action.action) {
            case 'L':
                currentDirection = getNewDirection(currentDirection, -action.value);
                break;
            case 'R':
                currentDirection = getNewDirection(currentDirection, action.value);
                break;
            case 'F':
                currentPosition = getNewPosition(currentDirection, currentPosition, action.value);
                break;
            default:
                currentPosition = getNewPosition(action.action, currentPosition, action.value);
                break;
        }

        return move(actions, ++index, currentPosition,  currentDirection);
    } else {
        return Math.abs(currentPosition.x) + Math.abs(currentPosition.y);
    }
}

const position = move(actions, 0, { x: 0, y: 0 }, 'E');

console.log(position);