function startGame() {
    let playerName:string = 'Audrey';
    logPlayer(playerName);

    let messageElement = document.getElementById('messages');
    messageElement!.innerText = 'Welcome to MultiMath! Starting new game...!!';
}

function logPlayer(name:string){
    console.log(`New game starting for player: ${name}`);
}

document.getElementById('startGame')!.addEventListener('click', startGame);