# DnDCardDesigner

A simple app for creating DnD cards

With the app you can:

- Create character deck
- Create spell decks
- Create item decks
- Create minion decks

To start working, just run the .exe located in the folder.

## Manual

Use the menus on the left side to navigate through the app.

You have to manualy click on "Update" to save changes.

Since the app is still in early stages, I would recomend to regularily check the home page to see if updates are correctly applied.

### Database

In case of app crashing on start, there is likely something wrong with the database.
You could open the database with an application like "Db browser for SQLite" and try and fix it.
Otherwise you will be forced to delete the database and loose all data.
The databse is in the file "carddesign.db".

Since the app is still in very eraly stage it is recomended to regularly make backups by copying database to another location.
This way, if database gets corrupted, you can just replace the corrupt database with the backup you made.

### A note on New/Update/Delete control

This control is found throughout the entire app and its core function remains similar.

Whenever you click on "New" you are required to enter a name. 
This is not a name of character or title of a card, but rather an unique name that can help you distinguish between different cards.

Whenever you make changes, you need to manualy click "Update" for the changes to save.

If you no longer need an item, you can press "Delete" and this will delete it.
Deleting a character does not delete its decks and deleting a deck does not delete its cards.

### Custom data

The app comes with only basic data and you will manualy need to add more.
When you add more data, you will need to restart the app for changes to take effect.

Custom data can be found in the Resources folder and includes:

- Armour
- Classes
- Clothing
- Consumables
- Usables
- Weapons

All this data is in json format and some basic placeholder data is already filled in.
Just follow examples to add more stuff.

Note that it is possible to "corrupt" the files by writing something the app can't parse.
It is recomended to make a backup in this case. 
It is also recomended to use an app that can compare two files and show changes (Meld, notepad++). 
This way you can quickly see what has changed between original file and your modified one.

Images for various avatars and icons can be picked from anywhere within your computer, however it is recomended that you put them in the Images folder.
This way you will have no worries over images getting lost if you move the app to another folder.

### Home

Home view of the application. Here you can view all your characters and their decks.
The tree view in the center shows characters and all its decks.
On the right side you can preview all the cards.
You can also use the buttons on top right to quickly navigate to appropriate screen where you can edit data.

### Character creator

Character creator allows you to create your character. 
To do so, you can click the "New" button on the top right of the screen.
The data entered here is basic data you would find on any character sheet.

### Minion creator

Minion creator allows you to make minion cards for characters like druid, or just some general monster cards for encounters.
Minion creator works similarly to the Character creator.

Note: this is the latest addition to the app and might still have some bugs :)

### Character card creator

Character card creator is where you start creating you Character base deck.
This deck will generaly hold the same data as you would find on a character sheet.

- First create a new item with the "New" button.
- Then pick a Character that will be used for the card.
- Next, pick the card type.
- Fill in any data.
- Hit "Update" to save the card.

### Minion card creator

Minion card creator follows the same idea as a Character card creator

### Spell card creator

Spell card creator works similar to other card creators.
Once you create a card for a spell, you don't need to create another card for same spell for some other character, if it also has this spell.
If you wish, you can create another card for higher level spells.

Note: If more characters use the same card and you choose to update it, this will change the card in all character decks!

### Item card creator

Same as other card creators. 
If you wish to add more items to the list of available items, you will need to modify the appropriate .json file in the Resources folder.

### Deck creator

Here you can create decks and assign cards to them.
Like before, use the "New" button to add a new deck.
If you rename the deck, you also need to click on "Update" so that changes take effect.
Under each deck you will see two columns.
The column of the left side shows all available cards.
To assign a card to a deck, simply click on it on the list.
The column on the right side shows cards currently in deck.
To remove a card from the deck, simply click on it in the list.

Note: Changing deck cards automaticaly updates cards and you do not have to click on the "Update button"

In the bottom half you can then assign decks to characters. The functionality is the same as for the upper half.

### Design card

In the Design card menu, you can change the colors of cards and their back sides.
The design will be applied to the character that is selected on the top right of the screen.
Background design is shared for all cards in a character deck, but each deck can have custom colors for the foregorund.

### Print layout

With the print layout you can save your decks to PDF so that they can later be printed.
Choose a deck you wish to have printed.
Choose your card scale and X offset.
Click on "Create PDF" to save the PDF.

X offset is an offset by which the backside cards will be shifted to the right.
This is because of the printer missalignment when they print in two-sided mode.
The front sides will not be printed on the same space as back side and once you cut out the cards, you will have white space around them.
In an attempt to minimize this, you can shift the backside to the right.
This will be more of a trial and error to get the right value.