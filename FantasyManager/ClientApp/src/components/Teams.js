import React, { Component } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

export class Teams extends Component {
    static displayName = Teams.name;

    constructor(props) {
        super(props);
        this.state = {
            teams: [],
            editModal: false,
            deleteModal: false,
            teamToEdit: {},
            teamToDelete: {}
        };

        fetch('api/team/list')
            .then(response => {
                return response.json();
            })
            .then(data => {
                this.setState({
                    teams: data
                });
            });

        this.toggleEditModel = this.toggleEditModel.bind( this );
        this.toggleDeleteModel = this.toggleDeleteModel.bind(this);

        this.updateTeam = this.updateTeam.bind( this );
        this.deleteTeam = this.deleteTeam.bind( this );
    }

    toggleEditModel( team ) {
        this.setState({
            editModal: !this.state.editModal,
            teamToEdit: team
        });
    }

    toggleDeleteModel( team ) {
        this.setState({
            deleteModal: !this.state.deleteModal,
            teamToDelete: team
        });
    }

    updateTeam( team ) {
        fetch('api/team/update')
            .then(response => {
                return response.json();
            })
            .then(data => {
                this.setState({
                    teams: data,
                    loading: false
                });
            });

        this.setState({
            editModal: !this.state.editModal
        });
    }

    deleteTeam() {
        var me = this;
        fetch( 'api/team/delete', {
            method: 'POST',
            body: JSON.stringify( me.state.teamToDelete ),
            headers: {
                'Content-Type': 'application/json'
            }
        } )
            .then( response => {
                return response.json();
            } )
            .then( data => {
                if ( data.isCompletedSuccessfully ) {
                    debugger;
                    for ( var i = 0; i < me.state.teams.length; i++ ) {
                        if ( JSON.stringify( me.state.teams[i] ) === JSON.stringify( me.state.teamToDelete ) ) {
                            me.state.teams.splice( i, 1 );
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
                <h1>NFL Teams</h1>
                <p>This simple React component demonstrates fetching data from the server via the Teams API. The user interface was built using Reactstrap.</p>
                <br /><br />
                <table className='table table-striped'>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>&nbsp;</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.teams && this.state.teams.map(team =>
                            <tr key={team.name}>
                                <td>{team.name}</td>
                                <td className="text-right">
                                    <button className="btn btn-success" onClick={() => this.toggleEditModel( team )}>Edit</button>
                                    &nbsp;
                                    <button className="btn btn-danger" onClick={() => this.toggleDeleteModel( team )}>Delete</button>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>

                <Modal isOpen={this.state.editModal} toggle={this.toggleEditModel}>
                    <ModalHeader>Edit Team</ModalHeader>
                    <ModalBody>
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                    </ModalBody>
                    <ModalFooter>
                        <Button color='success' onClick={this.updateTeam}>Save</Button>
                        <Button color='danger' onClick={this.toggleEditModel}>Cancel</Button>
                    </ModalFooter>
                </Modal>

                <Modal isOpen={this.state.deleteModal} toggle={this.toggleDeleteModel}>
                    <ModalHeader>Delete Team</ModalHeader>
                    <ModalBody>
                        Are you sure you want to delete this team?
                    </ModalBody>
                    <ModalFooter>
                        <Button color='danger' onClick={this.deleteTeam}>Yes</Button>
                        <Button color='primary' onClick={this.toggleDeleteModel}>Cancel</Button>
                    </ModalFooter>
                </Modal>
            </div>
        );
    }
}
