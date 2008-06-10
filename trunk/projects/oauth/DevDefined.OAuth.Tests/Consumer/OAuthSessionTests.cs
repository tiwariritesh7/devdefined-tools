﻿using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using NUnit.Framework;

namespace DevDefined.OAuth.Tests.Consumer
{
    [TestFixture]
    public class OAuthSessionTests
    {
        [Test]
        public void GetUserAuthorizationUriForTokenWithCallback()
        {
            var session = new OAuthSession(new OAuthConsumerContext(), "http://localhost/request",
                                           "http://localhost/userauth", "http://localhost/access");
            string actual = session.GetUserAuthorizationUrlForToken(new TokenBase {Token = "token"},
                                                                    "http://localhost/callback");
            Assert.AreEqual(
                "http://localhost/userauth?oauth_token=token&oauth_callback=http%3A%2F%2Flocalhost%2Fcallback", actual);
        }

        [Test]
        public void GetUserAuthorizationUriForTokenWithoutCallback()
        {
            var session = new OAuthSession(new OAuthConsumerContext(), "http://localhost/request",
                                           "http://localhost/userauth", "http://localhost/access");
            string actual = session.GetUserAuthorizationUrlForToken(new TokenBase {Token = "token"}, null);
            Assert.AreEqual("http://localhost/userauth?oauth_token=token", actual);
        }
    }
}