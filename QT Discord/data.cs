using System;

public class Class1
{
	public Class1()
	{
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://api.imgur.com");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "5b1e99654c48d95d0bb253b3accdcfe57b2f3b87");
        var result = client.GetAsync("3/gallary/r" + "cute").Result;
        var data = JObject.Parse(await result.Content.ReadAsStringAsync());
        var images = data["data"] as JArray;
        if (images.Count == 0)
        {
            return "No images found.";
        }
        return images[new Random().Next(0, images.Count)]["link"].Value<string>(); ;
    }
}
