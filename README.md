# AspNetCore.2.InMemoryIdentity
Baseline app that has in inmemory identity store.  

# Integration

## Baseline out of the box webapp.
[baseline](https://github.com/ghstahl/AspNetCore.2.InMemoryIdentity/commit/f8a343553662838496f8820c8c61db2b0aa63053#diff-f03d32421dcf0fffab0ddc6e1c8a0986)

## Integration
[integrated](https://github.com/ghstahl/AspNetCore.2.InMemoryIdentity/commit/4084780864095c4c54789d385c6c2a3fe7fce2b0#diff-f03d32421dcf0fffab0ddc6e1c8a0986)

# Obvious stuff
This is meant for development.  The user store disappears when you restart the webapp.

# In Production
I have always been an enterprise developer, so the user database was always a different service.  No databases.  No need for any of the identity services, like 2FA, password reset, email verification, etc.   Too bad for me, because it is really well done and really cool.

You can still use the InMemoryUserStore in production.  Even at scale.  Its there to trick the identity framework to do what it wants to do.

[External Identity Reference](src/ReferenceWebApp.ExternalIdentity)
