function startGame() {
    var messageElement = document.getElementById('messages');
    messageElement!.innerText = 'Welcome to MultiMath! Starting new game...';
}
//hello

document.getElementById('startGame')!.addEventListener('click', startGame);