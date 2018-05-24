import React, { Component } from 'react';

class SalesAccountAddress extends Component {
    render() {
        const { address = {} } = this.props;
        return (
            address
                ? <div>
                {Object.keys(address)
                    .map((k, i) => k !== 'countryUri' && k !== 'postcode' && <div key={i}>{address[k]}</div>)}  
                {<div>{address.postcode}</div>}             
                </div>
                : <div></div>
        );
    }
}

export default SalesAccountAddress;