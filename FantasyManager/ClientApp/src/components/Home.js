import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <p>Welcome to FantasyManager, a single-page application, built with:</p>
        <ul>
          <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
          <li><a href='https://facebook.github.io/react/'>React</a> for client-side code</li>
          <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
        </ul>

        <br />

        <h2>Challenge</h2>
        <p>Create a .NET Core website using an in-memory data storage solution that allows users to browse real or fictitious teams and their roster, as well as a simple management interface to add, update, and remove team members and teams. You may use any inspiration for your teams (ESports, Soccer, NFL, Games, D&D Adventuring Parties, etc.)</p>

        <h2>Technologies</h2>
        <p>Please use any version of .NET Core for the development of this challenge. You may use any libraries you choose to enhance the application.</p>

        <h2>User Stories</h2>
        <ul>
            <li>As a visitor to the site, I would like to be able to browse teams, see their logo, name and then navigate to a list of players.</li>
            <li>As a visitor to the site, I should be able to see the player's picture or avatar, name, and important statistics (You can make up values, there is no need to look up statistics on Fandom for this challenge) in a detail view.</li>
            <li>As a contributor, I should be able to add new teams and players and add players to a team.</li>
            <li>As a contributor, I should be able to edit and remove teams and players and their statistics and avatars.</li>
            <li>It is not a requirement to add authentication or security to the application.</li>
        </ul>

        <h2>Known Issues</h2>
        <ul>
            <li>When deleting a player, the player's statistic isn't delting from the database</li>
            <li>A player can only have one statistic associated with them. ESPN and other sites allow a player to have statstics that span multiple years. To fix this I will need to create a join table to allow a player to have associations to multiple statistics.</li>
            <li>Player avatar fails to upload. I will need to investigate this further to determine why it's failing.</li>
        </ul>
      </div>
    );
  }
}
