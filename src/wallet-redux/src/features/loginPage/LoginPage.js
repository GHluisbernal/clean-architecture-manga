import React, { Component } from 'react';
import { Link, Redirect } from 'react-router-dom';
import { connect } from 'react-redux';
import "../../App.css";

class LoginPage extends Component {
    componentDidMount() {
        document.title = 'React Login';
    }

    loginWithGitHub() {
        window.location.href = `https://localhost:/api/v1/Login/GitHub?ReturnUrl=${window.location.href}`;
    }

    loginWithGoogle() {
        window.location.href = `/api/v1/Login/Google?ReturnUrl=${window.location.href}`;
    }

    render() {
        let isSuccess, message;

        // if (this.props.response.login.hasOwnProperty('response')) {
        //   isSuccess = this.props.response.login.response.success;
        //   message = this.props.response.login.response.message;

        //   if (isSuccess) {
        //     //setCookie('token', this.props.response.login.response.token, 1);
        //   }
        // }

        return (
          <div>
            <h3>Login Page</h3>
            {!isSuccess ? <div>{message}</div> : <Redirect to='dashboard' />}
            <button className="text-dark nav-link" onClick={this.loginWithGitHub.bind(this)}>Login with GitHub</button>
            <button className="text-dark nav-link" onClick={this.loginWithGoogle.bind(this)}>Login with Google</button>
          </div>
        );
      }
}

export default LoginPage;
