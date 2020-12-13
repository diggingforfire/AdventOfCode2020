const actions = require("fs")
    .readFileSync(require("path").join(__dirname, "input.txt"), "utf8")
    .split("\r\n")
    .map(line => { return {
            action: line[0], 
            value: Number(line.slice(1, line.length)) 
        };});

function getNewPosition(direction, waypointPosition, value) {
    switch (direction) {
        case 'N':
            waypointPosition.y -= value;
            break;
        case 'E':
            waypointPosition.x += value;
            break;
        case 'S':
            waypointPosition.y += value;
            break;
        case 'W':
            waypointPosition.x -= value;
            break;
    }

    return waypointPosition;
}

function rotatePosition(mod, waypointPosition, value) {
    let tmpY;
    switch (value) {
        case 90:
            tmpY = waypointPosition.y;
            waypointPosition.y = waypointPosition.x * mod;
            waypointPosition.x = -tmpY * mod;
            break;

        case 180:
            waypointPosition.x = -waypointPosition.x;
            waypointPosition.y = -waypointPosition.y;
            break;

        case 270:
            tmpY = waypointPosition.y;
            waypointPosition.y = -waypointPosition.x * mod;
            waypointPosition.x = tmpY * mod;
            break;
    }

    return waypointPosition;
}

function move(actions, index, shipPosition, waypointPosition) {
    const action = actions[index];
    if (action) {
        switch (action.action) {
            case 'L':
                waypointPosition = rotatePosition(-1, waypointPosition, action.value);
                break;
            case 'R':
                waypointPosition = rotatePosition(1, waypointPosition, action.value);
                break;
            case 'F':
                shipPosition.x += action.value * waypointPosition.x;
                shipPosition.y += action.value * waypointPosition.y;
                break;
            default:
                waypointPosition = getNewPosition(action.action, waypointPosition, action.value);
                break;
        }

        return move(actions, ++index, shipPosition, waypointPosition);
    } else {
        return Math.abs(shipPosition.x) + Math.abs(shipPosition.y);
    }
}

const result = move(actions, 0, { x: 0, y: 0 }, { x: 10, y: -1 });

console.log(result);