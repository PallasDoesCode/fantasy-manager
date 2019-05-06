import React, { Component } from 'react';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { teams: [], loading: true };

        fetch('api/team/list')
            .then( response => {
                console.log(`response: ${JSON.stringify(response)}`);
                response.json()
            } )
            .then( data => {
                console.log( `data: ${data}` );
                this.setState({ teams: data, loading: false });
            } );
    }

    static renderTeams( teams ) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Name</th>
                    </tr>
                </thead>
                <tbody>
                    {teams.map(team =>
                        <tr key={team.name}>
                            <td>{team.name}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        if (this.state.teams == [])
            console.log("teams is an empty array");
        let contents = this.state.loading && this.state.teams !== []
            ? <p><em>Loading...</em></p>
            : FetchData.renderTeams( this.state.teams );

        return (
            <div>
                <h1>Teams</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }
}
