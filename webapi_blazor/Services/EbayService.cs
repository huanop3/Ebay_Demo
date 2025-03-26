
public class EbayService
{
    public List<EbayProductVM> EbayLists = new List<EbayProductVM>();
    public List<EbayProductVM> EbayAllLists = new List<EbayProductVM>();
    public int TotalProduct { get; set; }
    public HttpClient _httpClient;
    public EbayService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task EbayGetByCategory(string? sort)
    {
        if (sort == "All" || sort == "" || sort == null)
        {
            var url1 = "http://localhost:9999/Ebay/Getall";
            var res1 = await _httpClient.GetFromJsonAsync<List<EbayProductVM>>(url1);
            TotalProduct = res1.Count;
            EbayLists = res1;

        }
        else
        {
            var url = $"http://localhost:9999/Ebay/GetByCategory?category={sort}";
            var res = await _httpClient.GetFromJsonAsync<List<EbayProductVM>>(url);
            TotalProduct = res.Count;
            EbayLists = res;

        }
        NotifyStateChanged();
    }
    public async Task SearchByKeyword(string? keyword)
    {
        if (keyword == "" || keyword == null)
        {
            EbayGetByCategory("All");
        }
        else
        {
            var url = $"http://localhost:9999/Ebay/SearchByKeyword?keyword={keyword}";
            var res = await _httpClient.GetFromJsonAsync<List<EbayProductVM>>(url);
            TotalProduct = res.Count;
            EbayLists = res;
            NotifyStateChanged();
        }
    }
    public async Task SearchByKeywordAndCategory(string? keyword, string? category)
    {
        if (keyword == "" || keyword == null)
        {
            EbayGetByCategory(category);
        }
        else if(category == "All"){
            await SearchByKeyword(keyword);
        }
        else
        {
            var url = $"http://localhost:9999/Ebay/SearchByKeywordAndCategory?keyword={keyword}&category={category}";
            var res = await _httpClient.GetFromJsonAsync<List<EbayProductVM>>(url);
            TotalProduct = res.Count;
            EbayLists = res;
            NotifyStateChanged();
        }
    }
    public async Task SortByPrice(string? sort,string? category,string? keyword)
    {
        if (sort == "All" || sort == "" || sort == null)
        {
            SearchByKeywordAndCategory(keyword,category);
        }
        else if (sort == "Low to High")
        {
            EbayLists = EbayLists.OrderBy(p => p.Price).ToList();
            NotifyStateChanged();
        }
        else if (sort == "High to Low")
        {
            EbayLists = EbayLists.OrderByDescending(p => p.Price).ToList();
            NotifyStateChanged();
        }
        NotifyStateChanged();
    }
    public event Action OnChange;
    private void NotifyStateChanged() => OnChange?.Invoke();
}