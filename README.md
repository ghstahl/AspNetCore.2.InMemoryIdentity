# AspNetCore.2.InMemoryIdentity
Baseline app that has in inmemory identity store.  

# Integration

## Baseline out of the box webapp.
[baseline](https://github.com/ghstahl/AspNetCore.2.InMemoryIdentity/commit/f8a343553662838496f8820c8c61db2b0aa63053#diff-f03d32421dcf0fffab0ddc6e1c8a0986)

## Integration
[integrated](https://github.com/ghstahl/AspNetCore.2.InMemoryIdentity/commit/4084780864095c4c54789d385c6c2a3fe7fce2b0#diff-f03d32421dcf0fffab0ddc6e1c8a0986)

I chose to use Google as my OpenIdentityConnect provider.  Add your Google credentials to secrets.json using "Manage User Secrets"

```
{
  "Google-ClientId": "{blah}.apps.googleusercontent.com",
  "Google-ClientSecret": "{another blah}",
}

```
[Google OpenId Connect](https://developers.google.com/identity/protocols/OpenIDConnect)  

# Obvious stuff
This is meant for development.  The user store disappears when you restart the webapp.  

## however....
you can still use it in production, under a very specific case.  


# In Production
I have always been an enterprise developer, so the user database was always a different service.  No databases.  No need for any of the identity services, like 2FA, password reset, email verification, etc.   Too bad for me, because it is really well done and really cool.

You can still use the InMemoryUserStore in production.  Even at scale.  Its there to trick the identity framework to do what it wants to do.

[External Identity Reference](src/ReferenceWebApp.ExternalIdentity)

This project removed everything that has to do with management.  It only assumes that you will have an external OIDC provider, like Google.

The technique is actually pretty simple.  Use the InMemoryUserStore as a temporary storage service until signin.  Then delete the user.
It basically makes the app think we have the full identity framework.  The InMemoryUserStore is overkill for this, but who cares, it is just dead code for this case.

```
[HttpGet]
[AllowAnonymous]
public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
{
    if (remoteError != null)
    {
        ErrorMessage = $"Error from external provider: {remoteError}";
        return RedirectToAction(nameof(Login));
    }
    var info = await _signInManager.GetExternalLoginInfoAsync();
    if (info == null)
    {
        return RedirectToAction(nameof(Login));
    }

    var query = from claim in info.Principal.Claims
        where claim.Type == ClaimTypes.Name || claim.Type == "name"
        select claim;
    var queryNameId = from claim in info.Principal.Claims
        where claim.Type == ClaimTypes.NameIdentifier
        select claim;
    var nameClaim = query.FirstOrDefault();
    var nameIdClaim = queryNameId.FirstOrDefault();

    // paranoid
    var leftoverUser = await _userManager.FindByEmailAsync(nameClaim.Value);
    if (leftoverUser != null)
    {
        await _userManager.DeleteAsync(leftoverUser); // just using this inMemory userstore as a scratch holding pad
    }
    // paranoid end

    var user = new ApplicationUser { UserName = nameIdClaim.Value, Email = nameClaim.Value };
    var result = await _userManager.CreateAsync(user);
    var newUser = await _userManager.FindByIdAsync(user.Id);
    await _userManager.AddClaimAsync(newUser, new Claim("custom-name", nameClaim.Value));
    if (result.Succeeded)
    {
        await _signInManager.SignInAsync(user, isPersistent: false);
        await _userManager.DeleteAsync(user); // just using this inMemory userstore as a scratch holding pad
        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
        return RedirectToLocal(returnUrl);

    }
    return RedirectToAction(nameof(Login));
}
```
later on, you can get the oidc stuff as normal.
```
public async Task<IActionResult> About()
{
    ViewData["Message"] = "Your application description page.";
    var result = HttpContext.User.Claims.Select(
        c => new ClaimType { Type = c.Type, Value = c.Value });

    if (User.Identity.IsAuthenticated)
    {
        string accessToken = await HttpContext.GetTokenAsync(IdentityConstants.ExternalScheme, "access_token");
        string idToken = await HttpContext.GetTokenAsync(IdentityConstants.ExternalScheme, "id_token");

        // Now you can use them. For more info on when and how to use the 
        // access_token and id_token, see https://auth0.com/docs/tokens
    }

    return View(result);
}
```

# Credits
[https://github.com/aspnet/Identity  ... test/Microsoft.AspNetCore.Identity.InMemory.Test](https://github.com/aspnet/Identity/tree/24d4694ec5f8aa8c83d340b51ac11a7925a33061/test/Microsoft.AspNetCore.Identity.InMemory.Test)  
I shamelessly copied this project as a starter.  

[aspnetboilerplate](https://aspnetboilerplate.com/)  
I copied some of their userstore methods to augment missing intergaces from the MongoDB implementation.
```
src/Abp.Zero/Authorization/Users/AbpUserStore.cs
```


