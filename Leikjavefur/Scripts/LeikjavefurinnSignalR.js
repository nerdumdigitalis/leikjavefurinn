$(document).ready(function () {
    var hub = $.connection.communication;
    var myTurn = false;
    var grid = [['', '', ''], ['', '', ''], ['', '', '']];
    var myPosition = 1;
    var gameGroup = '';
    var chatGroup = '';
    var move = 1;
    var numberOfPlayers = 1;
    var textArea = document.getElementById('ChatArea');

    //Chat Receive Functions
    hub.client.receiveMessage = function (message) {
        var text = document.createTextNode(message + '\n');
        textArea.appendChild(text);
        textArea.scrollTop = textArea.scrollHeight;
    };

    //TicTacToe Receive Functions
    hub.client.cellClicked = function (cellId) {
        var hisMark = getHisMark();
        addToGrid(cellId, hisMark);
        $("#" + cellId).text(hisMark);

        var isGameOver = checkIfGameOver();

        if (isGameOver == true) {
            //hub.server.saveWinOrLoss(getUserId(), getRealGameId(), "Lost");
            alert("Þú tapaðir, spilaðu aftur!");
            if (getMyPlayerNumber() == 1)
            {
                hub.server.deleteGameById(getGameGroup());
            }
        }
        else if(checkIfGameTied() == true){
            hub.server.saveWinOrLoss(getUserId(), getRealGameId(), "Tie");
            alert("Það er jafntefli!");
            if (getMyPlayerNumber() == 1) {
                hub.server.deleteGameById(getGameGroup());
            }
        }
        else {
            myTurn = true;
        }
    };

    //Snakes'N'Ladders Receive Functions
    hub.client.receiveRollValueAndNextPlayer = function (_oldPosition, _newPosition, _player, _isGameOver, _roll, snakeOrLadder, snakeOrLadderValue) {

        var oldPosition = 0;
        oldPosition = parseInt(_oldPosition);
        var newPosition = 0;
        newPosition = parseInt(_newPosition);
        var player = 0;
        player = parseInt(_player);
        var roll = 0;
        roll = parseInt(_roll);
        var isGameOver = "";
        isGameOver = _isGameOver;


        //Start by moving player to new position
        $("#rolltext").text("Leikmaður " + player + " kastar " + roll);
        var difference = newPosition - oldPosition;
        if (snakeOrLadder == "true")
        {
            if (snakeOrLadderValue > oldPosition)
            {
                var length = snakeOrLadderValue - oldPosition;
                for (var i = 1; i <= length; i++) {
                    var nextPos = $("#" + (oldPosition + i)).position();
                    $("#player" + player).animate({ 'top': nextPos.top + 'px', 'left': nextPos.left + 'px' }, 300, function () { });
                }
                var endPos = $("#" + (newPosition)).position();
                $("#player" + player).animate({ 'top': endPos.top + 'px', 'left': endPos.left + 'px' }, 1000, function () { });

            }
            else if (snakeOrLadderValue < oldPosition)
            {
                var nextPosition = oldPosition + 1;
                for (var i = nextPosition; i <= 30; i++) {
                    var nextPos = $("#" + i).position();
                    $("#player" + player).animate({ 'top': nextPos.top + 'px', 'left': nextPos.left + 'px' }, 300, function () { });
                }

                var length = 30 - snakeOrLadderValue;
                for (var i = 1; i < length; i++) {
                    var nextPos = $("#" + (oldPosition - i)).position();
                    $("#player" + player).animate({ 'top': nextPos.top + 'px', 'left': nextPos.left + 'px' }, 300, function () { });
                }
                var endPos = $("#" + (newPosition)).position();
                $("#player" + player).animate({ 'top': endPos.top + 'px', 'left': endPos.left + 'px' }, 1000, function () { });
            }
        }
        else if(difference > 0)
        {
            if ((oldPosition + roll) > 30) {
                var startSpot = oldPosition + 1;
                for (var i = startSpot; i <= 30; i++) {
                    var nextPos = $("#" + i).position();
                    $("#player" + player).animate({ 'top': nextPos.top + 'px', 'left': nextPos.left + 'px' }, 300, function () { });
                }

                for (var i = 29; i >= newPosition; i--) {
                    var nextPos = $("#" + i).position();
                    $("#player" + player).animate({ 'top': nextPos.top + 'px', 'left': nextPos.left + 'px' }, 300, function () { });
                }
            }
            else {
                for (var i = oldPosition++; i <= newPosition; i++) {
                    var nextPos = $("#" + i).position();
                    $("#player" + player).animate({ 'top': nextPos.top + 'px', 'left': nextPos.left + 'px' }, 300, function () { });
                }
            }
        }
        else if (difference < 0) {
            var nextPosition = oldPosition + 1;
            for (var i = nextPosition; i <= 30; i++)
            {
                var nextPos = $("#" + i).position();
                $("#player" + player).animate({ 'top': nextPos.top + 'px', 'left': nextPos.left + 'px' }, 300, function () { });
            }

            nextPosition = 29;
            for (var i = nextPosition; i >= newPosition; i--) {
                var nextPos = $("#" + i).position();
                $("#player" + player).animate({ 'top': nextPos.top + 'px', 'left': nextPos.left + 'px' }, 300, function () { });
            }
        }

        //find out if i should do next
        var nextPlayer = player;
        if (nextPlayer == numberOfPlayers){
            nextPlayer = 1;
        }
        else{
            nextPlayer++;
        }

        var myNumber = parseInt(getMyPlayerNumber());

        if (roll == 6)
        {
            if (player == myNumber)
            {
                if (isGameOver != "true") {
                    myTurn = true;
                    $("#dice").show();
                    myPosition = newPosition;
                }
            }
        }
        else if (nextPlayer == myNumber) {
            if (isGameOver != "true") {
                myTurn = true;
                $("#dice").show();
            }
        }
        else if (player == myNumber) {
            myPosition = newPosition;
        }

        if (isGameOver == "true" && player == myNumber){
            hub.server.saveWinOrLoss(getUserId(), getRealGameId(), "Won");
            alert("Þú vannst! til hamingju :)");
            if (getMyPlayerNumber() == 1) {
                hub.server.deleteGameById(getGameGroup());
            }
        }
        else if (isGameOver == "true" && player != myNumber){
            hub.server.saveWinOrLoss(getUserId(), getRealGameId(), "Lost");
            alert("Þú tapaðir... :(");
            if (getMyPlayerNumber() == 1) {
                hub.server.deleteGameById(getGameGroup());
            }
        }
    };

    hub.client.receivePlayerCount = function (playerCount, userName) {
        numberOfPlayers = playerCount;

        if (playerCount < getMinPlayers()) {
            $("#waitingForPlayers").text("Waiting for: " + (getMinPlayers() - playerCount) + " player");
        }
        else if (playerCount >= getMinPlayers()){
            if (getMyPlayerNumber() == 1) {
                $("#startGame").show();
                $("#waitingForPlayers").text("Þú mátt byrja leikin!");
            }
            else
                $("#waitingForPlayers").text("Biða eftir að leikurinn hefst!");
        }
        $("#playerslist").append(userName + "<p></p>");
    };

    hub.client.gameStarted = function() {
        $("#waitingForPlayers").text("Leikurin er hafinn!");
    };

    //Connect
    $.connection.hub.start().done(function () {

        $("#startGame").click(function () {
            //startGame();
            hub.server.activateGame(gameGroup);
            $("#startGame").hide();
            hub.server.startGame(gameGroup);
            myTurn = true;
            if (getGameName() == "TicTacToe"){
            }
            else if (getGameName() == "SnakesAndLadders") {
                $("#dice").show();
            }
            $("#waitingForPlayers").text("Leikurin er hafinn!");
        });

        /*$("#deleteGame").click(function () {
            deleteGame();
            $("#deleteGame").hide();
        });*/

        //Get Groups
        if (typeof (getGameGroup) === 'function') {
            gameGroup = getGameGroup();
            hub.server.sendPlayerCount(gameGroup, getMyPlayerNumber(), getMyUserName());
            numberOfPlayers = getMyPlayerNumber();
        }
        if (typeof (getChatGroup) === 'function')
            chatGroup = getChatGroup();

        if (gameGroup != ''){
            hub.server.join(gameGroup);
            chatGroup = gameGroup;
        }
        else if (chatGroup != '')
            hub.server.join(chatGroup);

        //Chat Send Functions
        $("#SendButton").click(function () {
            var userName = getUserName();
            if (userName != '') {
                var message = $('#TextBoxMessage').val();
                if (message != '') {
                    message = userName + ": ";
                    message += $('#TextBoxMessage').val();
                    hub.server.sendMessage(chatGroup, message);
                    $('#TextBoxMessage').val('');
                    var text = document.createTextNode(message + '\r');
                    textArea.appendChild(text);
                    textArea.scrollTop = textArea.scrollHeight;
                    document.getElementById("TextBoxMessage").focus();
                }
            }
            else {
                $('#TextBoxMessage').val('');
            }
        });

        //TicTacToe Send Functions
        $("#tictactoe tr td").click(function (event) {
            var elementId = event.target.id;
            var hisMark = getHisMark();
            var myMark = getMyMark();

            if (myTurn == true && $("#" + elementId).text() != hisMark && $("#" + elementId).text() != myMark) {
                myTurn = false;
                addToGrid(elementId, myMark);
                hub.server.clickCell(gameGroup, elementId);
                $("#" + elementId).text(myMark);
                if (checkIfGameOver() == true) {
                    hub.server.saveWinOrLoss(getUserId(), getRealGameId(), "Won");
                    alert("Þú vannst! til hamingju");
                    if (getMyPlayerNumber() == 1)
                    {
                        hub.server.deleteGameById(getGameGroup());
                    }
                }
                else if (checkIfGameTied() == true) {
                    hub.server.saveWinOrLoss(getUserId(), getRealGameId(), "Tie");
                    alert("Það er jafntefli!");
                    if (getMyPlayerNumber() == 1)
                    {
                        hub.server.deleteGameById(getGameGroup());
                    }
                }
            }
        });

        //Snakes'N'Ladders Send Functions
        $("#dice").click(function () {
            if (myTurn == true) {
                myTurn = false;
                hub.server.rollDice(gameGroup, getMyPlayerNumber(), myPosition);
                $("#dice").hide();
            }
        });

    });

    function addToGrid(id, mark) {
        if (id == 1)
            grid[0][0] = mark;
        else if (id == 2)
            grid[0][1] = mark;
        else if (id == 3)
            grid[0][2] = mark;
        else if (id == 4)
            grid[1][0] = mark;
        else if (id == 5)
            grid[1][1] = mark;
        else if (id == 6)
            grid[1][2] = mark;
        else if (id == 7)
            grid[2][0] = mark;
        else if (id == 8)
            grid[2][1] = mark;
        else if (id == 9)
            grid[2][2] = mark;
    };

    function checkIfGameOver() {
        if (grid[0][0] == grid[0][1] && grid[0][0] == grid[0][2] && grid[0][0] != '')
            return true;
        else if (grid[1][0] == grid[1][1] && grid[1][0] == grid[1][2] && grid[1][0] != '')
            return true;
        else if (grid[2][0] == grid[2][1] && grid[2][0] == grid[2][2] && grid[2][0] != '')
            return true;
        else if (grid[0][0] == grid[1][0] && grid[0][0] == grid[2][0] && grid[0][0] != '')
            return true;
        else if (grid[0][1] == grid[1][1] && grid[0][1] == grid[2][1] && grid[0][1] != '')
            return true;
        else if (grid[0][2] == grid[1][2] && grid[0][2] == grid[2][2] && grid[0][2] != '')
            return true;
        else if (grid[0][0] == grid[1][1] && grid[0][0] == grid[2][2] && grid[0][0] != '')
            return true;
        else if (grid[0][2] == grid[1][1] && grid[0][2] == grid[2][0] && grid[0][2] != '')
            return true;

        return false;
    };

    function checkIfGameTied() {
        if (grid[0][0] != "" && grid[0][1] != "" && grid[0][2] != ""
            && grid[1][0] != "" && grid[1][1] != "" && grid[1][2] != ""
            && grid[2][0] != "" && grid[2][1] != "" && grid[2][2] != "") {
            return true;
        }
    };
})

