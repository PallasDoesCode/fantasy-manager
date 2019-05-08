import React, { Component } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Form, FormGroup, Input, Label } from 'reactstrap';

export class Players extends Component {
    static displayName = Players.name;

    constructor(props) {
        super(props);
        this.state = {
            players: [],
            profileModal: false,
            newModal: false,
            editModal: false,
            deleteModal: false,
            playerToView: {
                statistics: {}
            },
            playerToEdit: {},
            playerToDelete: {}
        }

        fetch('api/player/list')
            .then( response => {
                return response.json();
            } )
            .then( data => {
                this.setState(
                {
                    players: data
                } );
            } );

        this.toggleProfileModel = this.toggleProfileModel.bind( this );
        this.toggleNewModel = this.toggleNewModel.bind( this );
        this.toggleEditModel = this.toggleEditModel.bind( this );
        this.toggleDeleteModel = this.toggleDeleteModel.bind( this );

        this.updatePlayer = this.updatePlayer.bind( this );
        this.deletePlayer = this.deletePlayer.bind( this );
    }

    toggleNewModel() {
        this.setState( {
            newModal: !this.state.newModal
        } );
    }


    toggleProfileModel( player ) {
        this.setState( {
            profileModal: !this.state.profileModal,
            playerToView: player
        } );
    }

    toggleEditModel( player ) {
        this.setState({
            editModal: !this.state.editModal,
            playerToEdit: player
        } );
    }

    toggleDeleteModel( player ) {
        this.setState({
            deleteModal: !this.state.deleteModal,
            playerToDelete: player
        } );
    }

    newPlayer() {
        var me = this;
        var player = {
            name: document.getElementsByName( 'newName' )[0].value,
            avatar: document.getElementsByName( 'newAvatarPicker' )[0].files[0],
            height: document.getElementsByName( 'newHeight' )[0].value,
            weight: document.getElementsByName( 'newWeight' )[0].value,
            position: document.getElementsByName( 'newPosition' )[0].value,
            status: document.getElementsByName( 'newStatus' )[0].value,
            statistics: {
                gamesPlayed: document.getElementsByName( 'newGamesPlayed' )[0].value,
                receivingYards: document.getElementsByName( 'newReceivingYards' )[0].value,
                yardsPerReception: document.getElementsByName( 'newYardsPerReception' )[0].value
            }
        }

        fetch( 'api/player/create', {
            method: 'POST',
            body: JSON.stringify( player ),
            headers: {
                'Content-Type': 'application/json'
            }
        } )
            .then( response => {
                return response.json();
            } )
            .then( data => {
                if ( data.isCompletedSuccessfully ) {
                    me.stats.players.push( player );

                    me.forceUpdate();
                }
            } );
    }

    updatePlayer() {
        var me = this;
        var avatar = document.getElementsByName( 'avatarPicker' )[0].files[0];
        var gamesPlayed = document.getElementsByName( 'gamesPlayed' )[0].value;
        var receivingYards = document.getElementsByName( 'receivingYards' )[0].value;
        var yardsPerReception = document.getElementsByName( 'yardsPerReception' )[0].value;
        var player = this.state.playerToEdit;
        player.avatar = avatar;
        player.statistics.gamesPlayed = gamesPlayed;
        player.statistics.receivingYards = receivingYards;
        player.statistics.yardsPerReception = yardsPerReception;

        fetch( 'api/player/update', {
            method: 'POST',
            body: player,
            headers: {
                'Content-Type': 'application/json'
            }
        } )
        .then( response => {
            return response.json();
        })
        .then( data => {
            if ( data.isCompletedSuccessfully ) {
                // TODO: Update UI now that we've successfully deleted the record
                for ( var i = 0; i < me.state.players.length; i++ ) {
                    if ( JSON.stringify( me.state.players[i] ) === JSON.stringify( me.state.playerToDelete ) ) {
                        me.state.players[i] = player;
                    }
                }

                me.forceUpdate();
            }
        });

        this.setState({
            editModal: !this.state.editModal
        });
    }

    deletePlayer() {
        var me = this;
        fetch( 'api/player/delete', {
            method: 'POST',
            body: JSON.stringify( me.state.playerToDelete ),
            headers: {
                'Content-Type': 'application/json'
            }
        } )
        .then( response => {
            return response.json();
        } )
        .then( data => {
            if ( data.isCompletedSuccessfully ) {
                // TODO: Update UI now that we've successfully deleted the record
                for ( var i = 0; i < me.state.players.length; i++ ) {
                    if ( JSON.stringify( me.state.players[i] ) === JSON.stringify( me.state.playerToDelete ) ) {
                        me.state.players.splice( i, 1 );
                    }
                }

                me.forceUpdate();
            }
        } );

        this.setState( {
            deleteModal: !this.state.deleteModal
        } );
    }

    render() {
        return (
            <div>
                <h1>NFL Players</h1>
                <p>This simple React component demonstrates fetching data from the server via the Player API. The user interface was built using Reactstrap.</p>
                <br />
                <button className="btn btn-success" onClick={() => this.toggleNewModel()}>New Player</button>
                <br /><br />
                <table className='table table-striped'>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>&nbsp;</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.players && this.state.players.map(player =>
                            <tr key={player.name}>
                                <td>{player.name}</td>
                                <td className="text-right">
                                    <button className="btn btn-primary" onClick={() => this.toggleProfileModel( player )}>View Player Profile</button>
                                    &nbsp;
                                    <button className="btn btn-success" onClick={ () => this.toggleEditModel( player )}>Edit</button>
                                    &nbsp;
                                    <button className="btn btn-danger" onClick={ () => this.toggleDeleteModel (player) }>Delete</button>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>

                <Modal isOpen={this.state.newModal} toggle={this.toggleNewModel}>
                    <ModalHeader>Player Profile</ModalHeader>
                    <ModalBody>
                        <Form>
                            <FormGroup>
                                <Input type="file" name="newAvatarPicker" accept=".png" />
                            </FormGroup>
                            <FormGroup>
                                <Label for="newName">Name</Label>
                                <Input type="text" name="newName" />
                            </FormGroup>
                            <FormGroup>
                                <Label for="newHeight">Height (inches)</Label>
                                <Input type="text" name="newHeight" />
                            </FormGroup>
                            <FormGroup>
                                <Label for="newWeight">Weight</Label>
                                <Input type="text" name="newWeight" />
                            </FormGroup>
                            <FormGroup>
                                <Label for="newPosition">Position</Label>
                                <Input type="text" name="newPosition" />
                            </FormGroup>
                            <FormGroup>
                                <Label for="newStatus">Status</Label>
                                <Input type="text" name="newStatus" />
                            </FormGroup>
                            <FormGroup>
                                <Label for="newGamesPlayed">Games Played</Label>
                                <Input type="text" name="newGamesPlayed" />
                            </FormGroup>
                            <FormGroup>
                                <Label for="newReceivingYards">Receiving Yards</Label>
                                <Input type="text" name="newReceivingYards" />
                            </FormGroup>
                            <FormGroup>
                                <Label for="newYardsPerReception">Yards Per Reception</Label>
                                <Input type="text" name="newYardsPerReception" />
                            </FormGroup>
                        </Form>

                    </ModalBody>
                    <ModalFooter>
                        <Button color='success' onClick={this.newPlayer}>Save</Button>
                        <Button color='danger' onClick={this.toggleNewModel}>Cancel</Button>
                    </ModalFooter>
                </Modal>

                <Modal isOpen={this.state.profileModal} toggle={this.toggleProfileModel}>
                    <ModalHeader>Player Profile</ModalHeader>
                    <ModalBody>
                        <h1>{this.state.playerToView.name}</h1>
                        <img alt={this.state.playerToView.name} src={"data:image/png;base64," + this.state.playerToView.avatar} height="100" width="150" />
                        <br />< br />
                        <p>Height (inches): {this.state.playerToView.height}</p>
                        <p>Weight: {this.state.playerToView.weight}</p>
                        <p>Position: {this.state.playerToView.position}</p>
                        <p>Status: {this.state.playerToView.status}</p>
                        <p>Games Played: {this.state.playerToView.statistics.gamesPlayed}</p>
                        <p>Receiving Yards: {this.state.playerToView.statistics.receivingYards}</p>
                        <p>Yards Per Reception: {this.state.playerToView.statistics.yardsPerReception}</p>

                    </ModalBody>
                    <ModalFooter>
                        <Button color='success' onClick={() => this.toggleProfileModel( this.state.playerToView ) }>Close</Button>
                    </ModalFooter>
                </Modal>

                <Modal isOpen={this.state.editModal} toggle={this.toggleEditModel}>
                    <ModalHeader>Edit Player</ModalHeader>
                    <ModalBody>
                        <Form>
                            <FormGroup>
                                <Input type="file" name="avatarPicker" accept=".png" />
                            </FormGroup>
                            <FormGroup>
                                <Label for="gamesPlayed">Games Played</Label>
                                <Input type="text" name="gamesPlayed" />
                            </FormGroup>
                            <FormGroup>
                                <Label for="receivingYards">Receiving Yards</Label>
                                <Input type="text" name="receivingYards" />
                            </FormGroup>
                            <FormGroup>
                                <Label for="yardsPerReception">Yards Per Reception</Label>
                                <Input type="text" name="yardsPerReception" />
                            </FormGroup>
                        </Form>
                    </ModalBody>
                    <ModalFooter>
                        <Button color='success' onClick={this.updatePlayer}>Save</Button>
                        <Button color='danger' onClick={this.toggleEditModel}>Cancel</Button>
                    </ModalFooter>
                </Modal>

                <Modal isOpen={this.state.deleteModal} toggle={this.toggleDeleteModel}>
                    <ModalHeader>Delete Player</ModalHeader>
                    <ModalBody>
                        Are you sure you want to delete this player?
                    </ModalBody>
                    <ModalFooter>
                        <Button color='danger' onClick={this.deletePlayer}>Yes</Button>
                        <Button color='primary' onClick={this.toggleDeleteModel}>Cancel</Button>
                    </ModalFooter>
                </Modal>
            </div>
        );
    }
}
