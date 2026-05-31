# 09_MCP_STITCH_INTEGRATION.md

## Purpose
Explain how the Stitch design system integrates with the Furniture Store AI (Stitch MCP). This file will be used by developers to wire up the generated UI mockups to the backend services.

### Integration Steps
1. **Export Stitch Prompt** – The prompts defined in `08_STITCH_PROMPT.md` are sent to the Stitch MCP endpoint via HTTP POST.
2. **Receive Mockup** – Stitch returns a PNG image and a JSON descriptor (component hierarchy, CSS tokens).
3. **Store Assets** – Save the PNG under `wwwroot/images/stitch/` and the JSON under `wwwroot/stitch/`.
4. **Map Components** – Use the JSON to generate Razor partials or static HTML snippets. Each component maps to a controller action that supplies data via the Node.js API.
5. **Authentication** – All API calls made by the generated UI must include the JWT stored in the `AspNetCore.Culture` cookie (or Authorization header).
6. **Deploy** – Commit the generated Razor files to the repository; CI/CD will compile them with the rest of the MVC project.

### Sample Curl
```bash
curl -X POST https://stitch.example.com/api/v1/generate \
  -H "Content-Type: application/json" \
  -d '{"prompt": "Generate a high‑fidelity mockup for the Home page..."}'
```

The response contains `imageUrl` and `componentSpec`. Store them as described above.

---

*End of document.*
